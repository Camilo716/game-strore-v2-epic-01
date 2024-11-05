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
        byte[] content = JsonSerializer.SerializeToUtf8Bytes(game);

        return new GameFile()
        {
            FileName = $"_{game.Key}.txt",
            ContentType = "application/octet-stream",
            Content = new MemoryStream(content),
        };
    }
}