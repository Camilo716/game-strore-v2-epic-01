using GameStore.Core.Models;
using GameStore.Infraestructure.Data;

namespace GameStore.Tests.Seed;

public class GameSeed(GameStoreDbContext dbContext)
{
    private readonly GameStoreDbContext _dbContext = dbContext;

    public Game GearsOfWar => new()
    {
        Id = Guid.Parse("0a2bd33d-030a-4502-9806-c2fdd1b2c4fb"),
        Name = "Gears of War",
        Key = "GearsOfWar",
        Description = "Description",
        Genres =
        [
            _dbContext.Genres.Find(GenreSeed.Action.Id),
        ],
    };

    public List<Game> GetGames()
    {
        return
        [
            GearsOfWar,
        ];
    }
}