using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface IGameRepository
{
    public Task<Game> GetByIdAsync(Guid id);

    public Task<IEnumerable<Game>> GetAllAsync();

    public Task<Game> GetByKeyAsync(string key);

    public Task InsertAsync(Game game);

    Task DeleteByIdAsync(Guid id);

    void Update(Game game);
}