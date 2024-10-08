using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Repositories;

public class PlatformRepository(GameStoreDbContext dbContext) : IPlatformRepository
{
    private readonly GameStoreDbContext _dbContext = dbContext;

    public async Task<Platform> GetByIdAsync(Guid id)
    {
        return await _dbContext.Platforms.FindAsync(id);
    }

    public async Task<IEnumerable<Platform>> GetAllAsync()
    {
        return await _dbContext.Platforms.ToListAsync();
    }
}