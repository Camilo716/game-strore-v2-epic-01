using GameStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Data;

public class GameStoreDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Platform> Platforms { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<Game> Games { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
            .HasIndex(g => g.Key)
            .IsUnique();

        modelBuilder.Entity<Platform>()
            .HasIndex(p => p.Type)
            .IsUnique();

        modelBuilder.Entity<Genre>()
            .HasIndex(g => g.Name)
            .IsUnique();

        SeedGenres(modelBuilder);
        SeedPlatforms(modelBuilder);
    }

    private static void SeedGenres(ModelBuilder modelBuilder)
    {
        var strategyId = Guid.NewGuid();
        var rpgId = Guid.NewGuid();
        var sportsId = Guid.NewGuid();
        var actionId = Guid.NewGuid();
        var adventureId = Guid.NewGuid();
        var puzzleSkillId = Guid.NewGuid();

        // Seed main genres
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = strategyId, Name = "Strategy" },
            new Genre { Id = rpgId, Name = "RPG" },
            new Genre { Id = sportsId, Name = "Sports" },
            new Genre { Id = actionId, Name = "Action" },
            new Genre { Id = adventureId, Name = "Adventure" },
            new Genre { Id = puzzleSkillId, Name = "Puzzle & Skill" });

        // Subgenres
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = Guid.NewGuid(), Name = "RTS", ParentGenreId = strategyId },
            new Genre { Id = Guid.NewGuid(), Name = "TBS", ParentGenreId = strategyId },
            new Genre { Id = Guid.NewGuid(), Name = "Races", ParentGenreId = sportsId },
            new Genre { Id = Guid.NewGuid(), Name = "Rally", ParentGenreId = sportsId },
            new Genre { Id = Guid.NewGuid(), Name = "Formula", ParentGenreId = sportsId },
            new Genre { Id = Guid.NewGuid(), Name = "Off-road", ParentGenreId = sportsId },
            new Genre { Id = Guid.NewGuid(), Name = "FPS", ParentGenreId = actionId },
            new Genre { Id = Guid.NewGuid(), Name = "TPS", ParentGenreId = actionId });
    }

    private static void SeedPlatforms(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Platform>().HasData([
            new() { Type = "Mobile" },
            new() { Type = "Browser" },
            new() { Type = "Desktop" },
            new() { Type = "Console" },
        ]);
    }
}