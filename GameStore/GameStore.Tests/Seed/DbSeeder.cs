using GameStore.Infraestructure.Data;

namespace GameStore.Tests.Seed;

internal class DbSeeder
{
    internal static void SeedData(GameStoreDbContext context)
    {
        context.Platforms.AddRange(PlatformSeed.GetPlatforms());

        context.Genres.AddRange(GenreSeed.GetGenres());

        var games = GameSeed.GetGames();
        games.ForEach(g => g.Genres =
        [
            .. context.Genres
        ]);

        /*
        games
            .ForEach(game => game.Genres
                .ForEach(genre => context.Attach(genre).State = EntityState.Unchanged)); // .ForEach(genre => throw new Exception($"state of {game.Name}, {genre.Name},{context.Entry(genre).State}")));
        */

        context.Games.AddRange(games);
        context.SaveChanges();
    }
}