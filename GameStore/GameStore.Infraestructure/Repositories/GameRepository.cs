using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Repositories;

public class GameRepository(GameStoreDbContext dbContext)
    : BaseRepository<Game>(dbContext),
    IGameRepository
{
    public async Task<Game> GetByKeyAsync(string key)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Key == key);
    }

    public override async Task InsertAsync(Game entity)
    {
        entity.Genres.ForEach(g => DbContext.Entry(entity).State = EntityState.Unchanged);
        entity.Platforms.ForEach(g => DbContext.Entry(entity).State = EntityState.Unchanged);
        await base.InsertAsync(entity);
    }
}