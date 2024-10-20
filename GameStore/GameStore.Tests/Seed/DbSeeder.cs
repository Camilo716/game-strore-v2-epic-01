using GameStore.Core.Models;
using GameStore.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Tests.Seed;

internal class DbSeeder
{
    internal static void SeedData(GameStoreDbContext context)
    {
        context.Platforms.AddRange(PlatformSeed.GetPlatforms());

        context.Genres.AddRange(GenreSeed.GetGenres());

        var games = GameSeed.GetGames();
        AttachGenresToGames(context, games);
        context.Games.AddRange(games);

        context.SaveChanges();
    }

    private static void AttachGenresToGames(GameStoreDbContext context, List<Game> games)
    {
        games.ForEach(game =>
        {
            var genresOfGame = context.Genres
                .Where(g => game.Genres.Select(g => g.Id).Contains(g.Id))
                .ToList();

            game.Genres = genresOfGame;
        });
    }
}