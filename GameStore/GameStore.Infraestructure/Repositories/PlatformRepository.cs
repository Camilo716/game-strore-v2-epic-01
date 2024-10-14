using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Repositories;

public class PlatformRepository(GameStoreDbContext dbContext)
    : BaseRepository<Platform>(dbContext),
    IPlatformRepository
{
    public void Update(Platform platform)
    {
        var platformEntry = DbContext.Entry(platform);
        platformEntry.State = EntityState.Modified;
    }
}