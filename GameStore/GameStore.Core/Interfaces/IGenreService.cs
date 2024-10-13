using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface IGenreService
{
    public Task<Genre> GetByIdAsync(Guid id);
}