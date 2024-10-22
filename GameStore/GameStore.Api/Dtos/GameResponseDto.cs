using GameStore.Core.Models;

namespace GameStore.Api.Dtos;

public class GameResponseDto(Game game)
{
    private readonly Game _game = game;

    public Guid Id => _game.Id;

    public string Description => _game.Description;

    public string Key => _game.Key;

    public string Name => _game.Name;
}