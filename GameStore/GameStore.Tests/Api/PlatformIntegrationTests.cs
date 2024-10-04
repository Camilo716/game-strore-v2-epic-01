using System.Net;

namespace GameStore.Tests.Api;

public class PlatformIntegrationTests(CustomWebApplicationFactory factory)
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory = factory;

    [Fact]
    public async Task GetByIdReturnsSuccess()
    {
        using var client = _factory.CreateClient();

        var response = await client.GetAsync("api/platforms/1");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetByIdWithInvalidIdReturnsNotFound()
    {
        using var client = _factory.CreateClient();

        var response = await client.GetAsync("api/platforms/-1");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}