using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface IGenreRepository
{
    public Task<Genre> GetByIdAsync(Guid id);

    public Task<IEnumerable<Genre>> GetAllAsync();

    Task<IEnumerable<Genre>> GetByParentIdAsync(Guid parentId);

    Task DeleteByIdAsync(Guid id);
}