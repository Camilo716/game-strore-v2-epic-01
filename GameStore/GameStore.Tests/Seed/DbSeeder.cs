using GameStore.Core.Models;
using GameStore.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Tests.Seed;

internal class DbSeeder
{
    internal static void SeedData(GameStoreDbContext context)
    {
        context.Platforms.AddRange(PlatformSeed.GetPlatforms());

        var genres = GenreSeed.GetGenres();
        context.Genres.AddRange(genres);

        var games = GameSeed.GetGames();
        AttachGenresToGames(genres, games);
        context.Games.AddRange(games);

        context.SaveChanges();
    }

    private static void AttachGenresToGames(List<Genre> genres, List<Game> games)
    {
        games.ForEach(game =>
        {
            var genresOfGame = genres
                .Where(g => game.Genres.Select(g => g.Id).Contains(g.Id))
                .ToList();

            game.Genres = genresOfGame;
        });
    }
}