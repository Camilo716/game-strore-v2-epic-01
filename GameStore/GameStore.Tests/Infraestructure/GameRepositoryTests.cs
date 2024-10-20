using GameStore.Infraestructure.Data;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Infraestructure;

public class GameRepositoryTests
{
    [Fact]
    public async Task GetAll_ReturnsGamesInDatabase()
    {
        var dbContext = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var unitOfWork = new UnitOfWork(dbContext);

        var game = await unitOfWork.GameRepository.GetAllAsync();

        Assert.NotNull(game);
        Assert.Equal(GameSeed.GetGames().Count, game.Count());
    }

    [Fact]
    public async Task GetById_GivenValidId_ReturnsGamesInDatabase()
    {
        var dbContext = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var unitOfWork = new UnitOfWork(dbContext);
        Guid id = GameSeed.GearsOfWar.Id;

        var genre = await unitOfWork.GameRepository.GetByIdAsync(id);

        Assert.NotNull(genre);
        Assert.Equal(id, genre.Id);
    }
}