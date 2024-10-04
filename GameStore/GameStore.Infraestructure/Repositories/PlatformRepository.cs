using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Infraestructure.Data;

namespace GameStore.Infraestructure.Repositories;

public class PlatformRepository(GameStoreContext dbContext) : IPlatformRepository
{
    private readonly GameStoreContext _dbContext = dbContext;

    public async Task<Platform> GetByIdAsync(int id)
    {
        return await _dbContext.Platforms.FindAsync(id);
    }
}