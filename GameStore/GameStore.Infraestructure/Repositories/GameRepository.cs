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
            .Include(game => game.Platforms)
            .FirstOrDefault(game => game.Id == entity.Id)
            ?? throw new InvalidOperationException($"Game {entity.Id} was not found");

        DbContext.Entry(existingGame).CurrentValues.SetValues(entity);
        UpdateGenreRelationships(entity, existingGame);
        UpdatePlatformRelationships(entity, existingGame);

        base.Update(existingGame);
    }

    private void UpdatePlatformRelationships(Game entity, Game existingGame)
    {
        existingGame.Platforms.Clear();

        foreach (var platform in entity.Platforms)
        {
            var trackedPlatform = DbContext.Platforms
                .Find(platform.Id)
                ?? platform;

            existingGame.Platforms.Add(trackedPlatform);
        }
    }

    private void UpdateGenreRelationships(Game entity, Game existingGame)
    {
        existingGame.Genres.Clear();

        foreach (var genre in entity.Genres)
        {
            var trackedGenre = DbContext.Genres
                .Find(genre.Id)
                ?? genre;

            existingGame.Genres.Add(trackedGenre);
        }
    }
}