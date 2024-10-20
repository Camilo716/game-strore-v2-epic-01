using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Infraestructure.Data;

namespace GameStore.Infraestructure.Repositories;

public class GameRepository(GameStoreDbContext dbContext)
    : BaseRepository<Game>(dbContext),
    IGameRepository
{
}