using GameStore.Core.Models;

namespace GameStore.Tests.Seed;

public static class PlatformSeed
{
    public static List<Platform> GetPlatforms(bool includeIds = true)
    {
        return
        [
            new() { Id = includeIds ? 1 : default, Type = "Mobile" },
            new() { Id = includeIds ? 2 : default, Type = "Browser" },
            new() { Id = includeIds ? 3 : default, Type = "Desktop" },
            new() { Id = includeIds ? 4 : default, Type = "Console" },
        ];
    }
}