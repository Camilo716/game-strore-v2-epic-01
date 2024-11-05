using GameStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Data;

public static class PlatformSeeder
{
    public static void SeedPlatforms(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Platform>().HasData([
            new() { Type = "Mobile" },
            new() { Type = "Browser" },
            new() { Type = "Desktop" },
            new() { Type = "Console" },
        ]);
    }
}