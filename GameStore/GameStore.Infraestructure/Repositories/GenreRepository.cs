using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Repositories;

public class GenreRepository(GameStoreDbContext dbContext)
    : BaseRepository<Genre>(dbContext),
    IGenreRepository
{
    public async Task<IEnumerable<Genre>> GetByParentIdAsync(Guid parentId)
    {
        return await DbSet
            .Where(genre => genre.ParentGenreId == parentId)
            .ToListAsync();
    }
}