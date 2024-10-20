using GameStore.Core.Models;

namespace GameStore.Tests.Seed;

public static class GameSeed
{
    public static Game GearsOfWar => new()
    {
        Id = Guid.Parse("0a2bd33d-030a-4502-9806-c2fdd1b2c4fb"),
        Name = "Gears of War",
        Key = "GearsOfWar",
        Description = "Description",
        Genres =
        [
            GenreSeed.Action,
            GenreSeed.Shooter,
        ],
    };

    public static List<Game> GetGames()
    {
        return
        [
            GearsOfWar,
        ];
    }
}