using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Core.Services;
using GameStore.Tests.Seed;
using Moq;

namespace GameStore.Tests.Core;

public class GameServiceTest
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsGameModel()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var gameService = new GameService(unitOfWork.Object);
        Guid id = GameSeed.GearsOfWar.Id;

        var game = await gameService.GetByIdAsync(id);

        Assert.NotNull(game);
        Assert.Equal(game.Id, id);
    }

    [Fact]
    public async Task GetByKey_GivenValidKey_ReturnsGameModel()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var gameService = new GameService(unitOfWork.Object);
        string key = GameSeed.GearsOfWar.Key;

        var game = await gameService.GetByKeyAsync(key);

        Assert.NotNull(game);
        Assert.Equal(game.Key, key);
    }

    [Fact]
    public async Task GetByPlatformId_GivenValidPlatformId_ReturnsGames()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var gameService = new GameService(unitOfWork.Object);
        Guid platformId = PlatformSeed.Console.Id;

        var games = await gameService.GetByPlatformIdAsync(platformId);

        Assert.NotNull(games);
    }

    [Fact]
    public async Task GetAll_ReturnsGamesModels()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var gameService = new GameService(unitOfWork.Object);

        var games = await gameService.GetAllAsync();

        Assert.NotNull(games);
        Assert.Equal(GameSeed.GetGames().Count, games.Count());
    }

    [Fact]
    public async Task Create_GivenValidGame_CreatesGame()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var validGame = new Game() { Name = "Halo 3", Key = "Halo3" };
        var gameService = new GameService(unitOfWork.Object);

        await gameService.CreateAsync(validGame);

        unitOfWork.Verify(
            m => m.GameRepository.InsertAsync(It.Is<Game>(p => p.Name == validGame.Name)),
            Times.Once());

        unitOfWork.Verify(m => m.SaveChangesAsync(), Times.Once());
    }

    [Theory]
    [InlineData("Halo 3", "halo3")]
    public async Task Create_GivenGameWithoutKey_CreatesGameWithAutoGeneratedKey(string gameName, string expectedGeneratedKey)
    {
        // Arrange
        Game createdGame = null;
        Mock<IUnitOfWork> unitOfWork = new();
        unitOfWork
            .Setup(u => u.GameRepository.InsertAsync(It.IsAny<Game>()))
            .Callback<Game>(g => createdGame = g);

        var gameService = new GameService(unitOfWork.Object);
        var game = new Game() { Name = gameName };

        // Act
        await gameService.CreateAsync(game);

        // Assert
        Assert.Equal(expectedGeneratedKey, createdGame?.Key);
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesGame()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        Guid id = GameSeed.GetGames().First().Id;
        var gameService = new GameService(unitOfWork.Object);

        await gameService.DeleteAsync(id);

        unitOfWork.Verify(m => m.GameRepository.DeleteByIdAsync(id), Times.Once());
        unitOfWork.Verify(m => m.SaveChangesAsync(), Times.Once());
    }

    [Fact]
    public async Task Update_GivenValidGame_UpdatesGame()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var game = new Game() { Name = "Adventure" };
        var gameService = new GameService(unitOfWork.Object);

        await gameService.UpdateAsync(game);

        unitOfWork.Verify(
            m => m.GameRepository.Update(It.Is<Game>(p => p.Name == game.Name)),
            Times.Once());

        unitOfWork.Verify(m => m.SaveChangesAsync(), Times.Once());
    }

    private static Mock<IUnitOfWork> GetDummyUnitOfWorkMock()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork.Setup(m => m.GameRepository.GetAllAsync())
            .ReturnsAsync(GameSeed.GetGames());

        mockUnitOfWork.Setup(m => m.GameRepository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(GameSeed.GearsOfWar);

        mockUnitOfWork.Setup(m => m.GameRepository.GetByKeyAsync(It.IsAny<string>()))
            .ReturnsAsync(GameSeed.GearsOfWar);

        return mockUnitOfWork;
    }
}