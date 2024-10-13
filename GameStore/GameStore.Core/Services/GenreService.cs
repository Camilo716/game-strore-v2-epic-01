using GameStore.Core.Interfaces;
using GameStore.Core.Models;

namespace GameStore.Core.Services;

public class GenreService(IUnitOfWork unitOfWork)
    : IGenreService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Genre> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.GenreRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Genre>> GetAllAsync()
    {
        return await _unitOfWork.GenreRepository.GetAllAsync();
    }
}