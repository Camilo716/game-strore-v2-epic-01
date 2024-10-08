using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface IPlatformService
{
    public Task<Platform> GetByIdAsync(Guid id);

    public Task<IEnumerable<Platform>> GetAllAsync();
}