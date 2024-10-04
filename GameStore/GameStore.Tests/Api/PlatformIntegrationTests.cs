namespace GameStore.Tests.Api;

public class PlatformIntegrationTests(CustomWebApplicationFactory factory)
    : IClassFixture<CustomWebApplicationFactory>,
    IDisposable
{
    private readonly CustomWebApplicationFactory _factory = factory;

    [Fact]
    public async Task GetByIdReturnsSuccess()
    {
        using var client = _factory.CreateClient();

        var response = await client.GetAsync("api/platforms/1");

        response.EnsureSuccessStatusCode();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _factory.Dispose();
    }
}