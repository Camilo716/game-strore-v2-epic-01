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
        DbContext.AttachRange(entity.Genres);
        DbContext.AttachRange(entity.Platforms);
        await base.InsertAsync(entity);
    }

    public override void Update(Game entity)
    {
        var existingGame = DbSet
            .Include(game => game.Genres)
            .FirstOrDefault(game => game.Id == entity.Id);

        DbContext.Entry(existingGame!).CurrentValues.SetValues(entity);

        existingGame.Genres.Clear();

        foreach (var genre in entity.Genres)
        {
            var trackedGenre = DbContext.Genres
                .Find(genre.Id)
                ?? genre;

            existingGame.Genres.Add(trackedGenre);
        }

        base.Update(existingGame);
    }
}