using GameStore.Core.Models;

namespace GameStore.Api.Dtos;

public class PlatformResponseDto(Platform platform)
{
    private readonly Platform _platform = platform;

    public Guid Id => _platform.Id;

    public string Type => _platform.Type;
}