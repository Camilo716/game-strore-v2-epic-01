using GameStore.Core.Models;
using GameStore.Infraestructure.Data;

namespace GameStore.Infraestructure.Repositories;

public class PlatformRepository(GameStoreContext dbContext)
{
    private readonly GameStoreContext _dbContext = dbContext;

    public async Task<Platform> GetByIdAsync(int id)
    {
        return await _dbContext.Platforms.FindAsync(id);
    }
}