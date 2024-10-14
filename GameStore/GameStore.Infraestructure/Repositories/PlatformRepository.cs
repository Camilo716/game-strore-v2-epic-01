using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Infraestructure.Data;

namespace GameStore.Infraestructure.Repositories;

public class PlatformRepository(GameStoreDbContext dbContext)
    : BaseRepository<Platform>(dbContext),
    IPlatformRepository
{
}