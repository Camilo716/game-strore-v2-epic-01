using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Infraestructure.Data;

namespace GameStore.Infraestructure.Repositories;

public class GenreRepository(GameStoreDbContext dbContext)
    : BaseRepository<Genre>(dbContext),
    IGenreRepository
{
}