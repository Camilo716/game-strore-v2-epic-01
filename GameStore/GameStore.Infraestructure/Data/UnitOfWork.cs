using GameStore.Core.Interfaces;
using GameStore.Infraestructure.Repositories;

namespace GameStore.Infraestructure.Data;

public class UnitOfWork(GameStoreContext dbContext) : IUnitOfWork
{
    private readonly GameStoreContext _dbContext = dbContext;

    private IPlatformRepository _platformRepository;

    public IPlatformRepository PlatformRepository
    {
        get
        {
            _platformRepository ??= new PlatformRepository(_dbContext);
            return _platformRepository;
        }
    }
}