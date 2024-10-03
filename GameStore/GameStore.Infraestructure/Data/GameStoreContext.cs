using GameStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Data;

public class GameStoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Platform> Platforms { get; set; }
}