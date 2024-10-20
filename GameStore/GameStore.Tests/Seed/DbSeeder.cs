using GameStore.Infraestructure.Data;

namespace GameStore.Tests.Seed;

internal class DbSeeder
{
    internal static void SeedData(GameStoreDbContext context)
    {
        context.Platforms.AddRange(PlatformSeed.GetPlatforms());

        context.Genres.AddRange(GenreSeed.GetGenres());

        var gamesSeed = new GameSeed(context);

        context.Games.AddRange(gamesSeed.GetGames());
        context.SaveChanges();
    }
}