using GameStore.Core.Interfaces;
using GameStore.Core.Models;

namespace GameStore.Core.Services;

public class PlatformService(IUnitOfWork unitOfWork) : IPlatformService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<Platform>> GetAllAsync()
    {
        return await _unitOfWork.PlatformRepository.GetAllAsync();
    }

    public async Task<Platform> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.PlatformRepository.GetByIdAsync(id);
    }
}