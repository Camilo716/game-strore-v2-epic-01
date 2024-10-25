namespace GameStore.Api.Dtos.GameDtos;

public class GamePutRequest
{
    public SimpleGameWithIdDto Game { get; set; }

    public List<Guid> Genres { get; set; } =
    [
    ];

    public List<Guid> Platforms { get; set; } =
    [
    ];
}