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
        var platform = await _dbContext.Platforms.FindAsync(id);

        return platform;
    }

    public async Task<IEnumerable<Platform>> GetAllAsync()
    {
        return await _dbContext.Platforms.ToListAsync();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        Platform platform = await GetByIdAsync(id);
        _dbContext.Remove(platform);
    }

    public async Task InsertAsync(Platform platform)
    {
        await _dbContext.Platforms.AddAsync(platform);
    }

    public void Update(Platform platform)
    {
        var platformEntry = _dbContext.Entry(platform);
        platformEntry.State = EntityState.Modified;
    }
}