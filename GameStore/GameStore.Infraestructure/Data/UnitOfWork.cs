using GameStore.Core.Interfaces;
using GameStore.Infraestructure.Repositories;

namespace GameStore.Infraestructure.Data;

public class UnitOfWork(GameStoreDbContext dbContext) : IUnitOfWork
{
    private readonly GameStoreDbContext _dbContext = dbContext;

    private IPlatformRepository _platformRepository;
    private IGenreRepository _genreRepository;
    private IGameRepository _gameRepository;

    public IPlatformRepository PlatformRepository
    {
        get
        {
            _platformRepository ??= new PlatformRepository(_dbContext);
            return _platformRepository;
        }
    }

    public IGenreRepository GenreRepository
    {
        get
        {
            _genreRepository ??= new GenreRepository(_dbContext);
            return _genreRepository;
        }
    }

    public IGameRepository GameRepository
    {
        get
        {
            _gameRepository ??= new GameRepository(_dbContext);
            return _gameRepository;
        }
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}