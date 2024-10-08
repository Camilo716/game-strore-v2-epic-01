using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface IPlatformRepository
{
    public Task<Platform> GetByIdAsync(Guid id);

    public Task<IEnumerable<Platform>> GetAllAsync();

    Task DeleteByIdAsync(Guid id);

    Task InsertAsync(Platform validPlatform);
}