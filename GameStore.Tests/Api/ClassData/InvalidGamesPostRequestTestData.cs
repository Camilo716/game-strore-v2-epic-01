using System.Collections;
using GameStore.Api.Dtos.GameDtos;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Api.ClassData;

public class InvalidGamesPostRequestTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return
        [
            new GamePostRequest() { Game = null }
        ];

        SimpleGameDto invalidGameMissingName = GetValidGameDto();
        invalidGameMissingName.Name = null;
        yield return
        [
            new GamePostRequest() { Game = invalidGameMissingName }
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static SimpleGameDto GetValidGameDto()
    {
        return new()
        {
            Name = GameSeed.GearsOfWar.Name,
            Key = GameSeed.GearsOfWar.Key,
            Description = GameSeed.GearsOfWar.Description,
        };
    }
}