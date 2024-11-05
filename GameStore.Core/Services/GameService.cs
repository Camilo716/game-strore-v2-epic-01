using System.Text.Json;
using GameStore.Core.Interfaces;
using GameStore.Core.Models;

namespace GameStore.Core.Services;

public class GameService
    (IUnitOfWork unitOfWork) : IGameService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task CreateAsync(Game game)
    {
        GenerateGameKeyIfNotProvided(game);

        await _unitOfWork.GameRepository.InsertAsync(game);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        var games = await _unitOfWork.GameRepository.GetAllAsync();
        return games;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _unitOfWork.GameRepository.DeleteByIdAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(Game game)
    {
        _unitOfWork.GameRepository.Update(game);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Game> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.GameRepository.GetByIdAsync(id);
    }

    public async Task<Game> GetByKeyAsync(string key)
    {
        return await _unitOfWork.GameRepository.GetByKeyAsync(key);
    }

    public async Task<IEnumerable<Game>> GetByPlatformIdAsync(Guid platformId)
    {
        return await _unitOfWork.GameRepository.GetByPlatformIdAsync(platformId);
    }

    public async Task<IEnumerable<Game>> GetByGenreIdAsync(Guid genreId)
    {
        return await _unitOfWork.GameRepository.GetByGenreIdAsync(genreId);
    }

    private static void GenerateGameKeyIfNotProvided(Game game)
    {
        if (!string.IsNullOrWhiteSpace(game.Key))
        {
            return;
        }

        game.Key = JsonNamingPolicy.CamelCase.ConvertName(game.Name).Replace(" ", string.Empty);
    }
}