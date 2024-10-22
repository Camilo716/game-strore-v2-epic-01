using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface IGenreService
{
    public Task<Genre> GetByIdAsync(Guid id);

    public Task<IEnumerable<Genre>> GetAllAsync();

    public Task<IEnumerable<Genre>> GetByParentIdAsync(Guid parentId);

    public Task DeleteAsync(Guid id);

    Task CreateAsync(Genre genre);

    Task UpdateAsync(Genre genre);

    Task<IEnumerable<Genre>> GetByGameKeyAsync(string gameKey);
}