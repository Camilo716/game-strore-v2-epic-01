using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Core.Services;
using GameStore.Tests.Seed;
using Moq;

namespace GameStore.Tests.Core;

public class GameServiceTest
{
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

    private static Mock<IUnitOfWork> GetDummyUnitOfWorkMock()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork.Setup(m => m.GameRepository.GetAllAsync())
            .ReturnsAsync(GameSeed.GetGames());

        return mockUnitOfWork;
    }
}