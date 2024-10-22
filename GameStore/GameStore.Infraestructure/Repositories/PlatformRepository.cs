using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Repositories;

public class PlatformRepository(GameStoreDbContext dbContext)
    : BaseRepository<Platform>(dbContext),
    IPlatformRepository
{
    public async Task<IEnumerable<Platform>> GetByGameKeyAsync(string gameKey)
    {
        return await DbSet
            .Where(p => p.Games.Select(g => g.Id).Contains(p.Id))
            .ToListAsync();
    }
}