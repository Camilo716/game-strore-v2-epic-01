using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface IGameService
{
    public Task<IEnumerable<Game>> GetAllAsync();

    public Task CreateAsync(Game game);
}