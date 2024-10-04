using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Infraestructure.Data;

namespace GameStore.Infraestructure.Repositories;

public class PlatformRepository(GameStoreDbContext dbContext) : IPlatformRepository
{
    private readonly GameStoreDbContext _dbContext = dbContext;

    public async Task<Platform> GetByIdAsync(int id)
    {
        return await _dbContext.Platforms.FindAsync(id);
    }
}