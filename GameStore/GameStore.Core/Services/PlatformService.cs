using GameStore.Core.Interfaces;
using GameStore.Core.Models;

namespace GameStore.Core.Services;

public class PlatformService(IUnitOfWork unitOfWork)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Platform> GetByIdAsync(int id)
    {
        return await _unitOfWork.PlatformRepository.GetByIdAsync(id);
    }
}