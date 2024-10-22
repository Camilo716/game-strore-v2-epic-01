using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface IGameRepository
{
    public Task<Game> GetByIdAsync(Guid id);

    public Task<IEnumerable<Game>> GetAllAsync();

    public Task<Game> GetByKeyAsync(string key);
}