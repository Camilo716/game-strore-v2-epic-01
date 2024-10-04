using GameStore.Infraestructure.Data;
using GameStore.Tests.Seed;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Tests;

internal static class UnitTestHelper
{
    internal static DbContextOptions<GameStoreContext> GetUnitTestDbOptions()
    {
        var options = new DbContextOptionsBuilder<GameStoreContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new GameStoreContext(options);
        DbSeeder.SeedData(context);

        return options!;
    }
}