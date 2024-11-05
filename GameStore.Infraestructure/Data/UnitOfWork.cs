using GameStore.Core.Interfaces;
using GameStore.Infraestructure.Repositories;

namespace GameStore.Infraestructure.Data;

public class UnitOfWork(GameStoreDbContext dbContext) : IUnitOfWork
{
    private IPlatformRepository _platformRepository;
    private IGenreRepository _genreRepository;
    private IGameRepository _gameRepository;

    public IPlatformRepository PlatformRepository
    {
        get
        {
            _platformRepository ??= new PlatformRepository(DbContext);
            return _platformRepository;
        }
    }

    public IGenreRepository GenreRepository
    {
        get
        {
            _genreRepository ??= new GenreRepository(DbContext);
            return _genreRepository;
        }
    }

    public IGameRepository GameRepository
    {
        get
        {
            _gameRepository ??= new GameRepository(DbContext);
            return _gameRepository;
        }
    }

    private GameStoreDbContext DbContext => dbContext;

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}