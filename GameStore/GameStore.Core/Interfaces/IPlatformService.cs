using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface IPlatformService
{
    public Task<Platform> GetByIdAsync(Guid id);

    public Task<IEnumerable<Platform>> GetAllAsync();

    public Task DeleteAsync(Guid id);

    public Task CreateAsync(Platform platform);
}