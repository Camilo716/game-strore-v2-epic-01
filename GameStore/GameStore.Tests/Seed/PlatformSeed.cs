using GameStore.Core.Models;

namespace GameStore.Tests.Seed;

public static class PlatformSeed
{
    public static List<Platform> Platforms =>
        [
            new() { Id = 1, Type = "Mobile" },
            new() { Id = 2, Type = "Browser" },
            new() { Id = 3, Type = "Desktop" },
            new() { Id = 4, Type = "Console" },
        ];
}