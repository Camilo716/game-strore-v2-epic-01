using GameStore.Core.Models;

namespace GameStore.Tests.Seed;

public static class PlatformSeed
{
    public static List<Platform> Platforms =>
        [
            new() { Id = 1 },
        ];
}