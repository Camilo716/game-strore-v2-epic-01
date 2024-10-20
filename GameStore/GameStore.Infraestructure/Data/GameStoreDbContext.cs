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
        modelBuilder.Entity<Platform>().HasData([
            new() { Type = "Mobile" },
            new() { Type = "Browser" },
            new() { Type = "Desktop" },
            new() { Type = "Console" },
        ]);
    }
}