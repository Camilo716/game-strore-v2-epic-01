using GameStore.Core.Models;

namespace GameStore.Api.Dtos.GameDtos;

public class GameResponseDto(Game game)
{
    public Guid Id => Game.Id;

    public string Description => Game.Description;

    public string Key => Game.Key;

    public string Name => Game.Name;

    private Game Game => game;
}