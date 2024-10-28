using System.Text;
using System.Text.Json;
using GameStore.Core.Dtos;
using GameStore.Core.Interfaces;

namespace GameStore.Core.Services;

public class GameTextFileService(IUnitOfWork unitOfWork)
    : IGameFileService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<GameFile> GetByKeyAsync(string key)
    {
        var game = await _unitOfWork.GameRepository.GetByKeyAsync(key);

        string fileName = $"_{game.Key}.txt";
        string content = $"{fileName}\n{JsonSerializer.Serialize(game)}";

        var serializedGame = Encoding.UTF8.GetBytes(content);
        return new GameFile()
        {
            FileName = fileName,
            ContentType = "application/octet-stream",
            Content = new MemoryStream(serializedGame),
        };
    }
}