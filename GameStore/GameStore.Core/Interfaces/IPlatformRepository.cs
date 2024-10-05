using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface IPlatformRepository
{
    public Task<Platform> GetByIdAsync(Guid id);
}