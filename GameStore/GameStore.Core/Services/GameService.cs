using GameStore.Core.Interfaces;
using GameStore.Core.Models;

namespace GameStore.Core.Services;

public class GameService
    (IUnitOfWork unitOfWork) : IGameService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task CreateAsync(Game game)
    {
        await _unitOfWork.GameRepository.InsertAsync(game);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        var games = await _unitOfWork.GameRepository.GetAllAsync();
        return games;
    }
}