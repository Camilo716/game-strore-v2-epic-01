using GameStore.Core.Interfaces;
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

    private static Mock<IUnitOfWork> GetDummyUnitOfWorkMock()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork.Setup(m => m.GameRepository.GetAllAsync())
            .ReturnsAsync(GameSeed.GetGames());

        return mockUnitOfWork;
    }
}