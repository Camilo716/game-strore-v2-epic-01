using GameStore.Infraestructure.Data;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Infraestructure;

public class GameRepositoryTests
{
    [Fact]
    public async Task GetAll_ReturnsGamesInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);

        var game = await unitOfWork.GameRepository.GetAllAsync();

        Assert.NotNull(game);
        Assert.Equal(GameSeed.GetGames().Count, game.Count());
    }

    [Fact]
    public async Task GetById_GivenValidId_ReturnsGamesInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        Guid id = GameSeed.GearsOfWar.Id;

        var game = await unitOfWork.GameRepository.GetByIdAsync(id);

        Assert.NotNull(game);
        Assert.Equal(id, game.Id);
    }

    [Fact]
    public async Task GetByKey_GivenValidKey_ReturnsGameInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        const string key = "GearsOfWar";

        var game = await unitOfWork.GameRepository.GetByKeyAsync(key);

        Assert.NotNull(game);
        Assert.Equal(key, game.Key);
    }
}