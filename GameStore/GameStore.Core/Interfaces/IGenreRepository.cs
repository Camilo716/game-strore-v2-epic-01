using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface IGenreRepository
{
    public Task<Genre> GetByIdAsync(Guid id);
}