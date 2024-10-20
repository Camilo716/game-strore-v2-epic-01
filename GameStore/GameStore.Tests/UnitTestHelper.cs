using GameStore.Infraestructure.Data;
using GameStore.Tests.Seed;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Tests;

internal static class UnitTestHelper
{
    internal static DbContextOptions<GameStoreDbContext> GetUnitTestDbOptions()
    {
        var options = new DbContextOptionsBuilder<GameStoreDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        using var context = new GameStoreDbContext(options);
        DbSeeder.SeedData(context);

        return options!;
    }
}