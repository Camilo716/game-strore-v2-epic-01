using GameStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Data;

public class GameStoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Platform> Platforms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Platform>().Property(p => p.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<Platform>().HasData([
            new() { Id = 1, Type = "Mobile" },
            new() { Id = 2, Type = "Browser" },
            new() { Id = 3, Type = "Desktop" },
            new() { Id = 4, Type = "Console" },
        ]);
    }
}