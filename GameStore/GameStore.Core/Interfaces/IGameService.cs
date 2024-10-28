using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface IGameService
{
    public Task<Game> GetByIdAsync(Guid id);

    public Task<Game> GetByKeyAsync(string key);

    public Task<IEnumerable<Game>> GetAllAsync();

    public Task CreateAsync(Game game);

    public Task DeleteAsync(Guid id);

    Task UpdateAsync(Game game);
}