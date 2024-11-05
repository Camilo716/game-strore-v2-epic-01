namespace GameStore.Api.Dtos.GameDtos;

public class GamePostRequest
{
    public SimpleGameDto Game { get; set; }

    public List<Guid> Genres { get; set; } =
    [
    ];

    public List<Guid> Platforms { get; set; } =
    [
    ];
}