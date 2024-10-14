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

    public async Task<IEnumerable<Genre>> GetByParentIdAsync(Guid parentId)
    {
        return await _unitOfWork.GenreRepository.GetByParentIdAsync(parentId);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _unitOfWork.GenreRepository.DeleteByIdAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CreateAsync(Genre genre)
    {
        await _unitOfWork.GenreRepository.InsertAsync(genre);
        await _unitOfWork.SaveChangesAsync();
    }
}