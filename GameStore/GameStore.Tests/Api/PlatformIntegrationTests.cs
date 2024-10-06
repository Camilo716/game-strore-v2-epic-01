using System.Net;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Api;

public class PlatformIntegrationTests : BaseIntegrationTest
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsSuccess()
    {
        Guid id = PlatformSeed.GetPlatforms().First().Id;

        var response = await HttpClient.GetAsync($"api/platforms/{id}");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetById_GivenInvalidId_ReturnsNotFound()
    {
        var response = await HttpClient.GetAsync("api/platforms/-1");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}