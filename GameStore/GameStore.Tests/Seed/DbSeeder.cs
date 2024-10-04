using GameStore.Infraestructure.Data;

namespace GameStore.Tests.Seed;

internal class DbSeeder
{
    internal static void SeedData(GameStoreContext context)
    {
        context.Platforms.AddRange(PlatformSeed.GetPlatforms(includeIds: false));

        context.SaveChanges();
    }
}