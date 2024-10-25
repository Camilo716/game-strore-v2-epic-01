using System.Collections;
using GameStore.Api.Dtos.PlatformDtos;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Api.ClassData;

public class InvalidPlatformsPutRequestTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var validPlatform = new SimplePlatformWithIdDto()
        {
            Id = PlatformSeed.Mobile.Id,
            Type = PlatformSeed.Mobile.Type,
        };

        yield return
        [
            new PlatformPutRequest() { Platform = null }
        ];

        SimplePlatformWithIdDto invalidPlatformMissingType = validPlatform;
        invalidPlatformMissingType.Type = null;
        yield return
        [
            new PlatformPutRequest() { Platform = invalidPlatformMissingType }
        ];

        SimplePlatformWithIdDto invalidPlatformMissingId = validPlatform;
        invalidPlatformMissingId.Id = Guid.Empty;
        yield return
        [
            new PlatformPutRequest() { Platform = invalidPlatformMissingId }
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}