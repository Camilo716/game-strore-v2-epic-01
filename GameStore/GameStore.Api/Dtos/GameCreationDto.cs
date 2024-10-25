using GameStore.Core.Models;

namespace GameStore.Api.Dtos;

public class GameCreationDto
{
    public Game Game { get; set; }

    public List<Guid> GenresIds { get; set; } =
    [
    ];

    public List<Guid> PlatformsIds { get; set; } =
    [
    ];
}