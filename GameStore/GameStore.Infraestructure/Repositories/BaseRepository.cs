using GameStore.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Repositories;

public class BaseRepository<TEntity>
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;

    public BaseRepository(GameStoreDbContext dbContext)
    {
        DbContext = dbContext;
        _dbSet = DbContext.Set<TEntity>();
    }

    protected GameStoreDbContext DbContext { get; private set; }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        var dbRecord = await _dbSet.FindAsync(id);
        return dbRecord;
    }
}