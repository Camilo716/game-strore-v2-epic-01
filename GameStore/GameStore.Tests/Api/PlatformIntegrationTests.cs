using System.Net;
using GameStore.Infraestructure.Data;
using GameStore.Tests.Seed;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Tests.Api;

public class PlatformIntegrationTests : IDisposable
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _httpClient;

    public PlatformIntegrationTests()
    {
        _factory = new CustomWebApplicationFactory();
        _httpClient = _factory.CreateClient();

        using var scope = _factory.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<GameStoreDbContext>();
        DbSeeder.SeedData(context);
    }

    [Fact]
    public async Task GetByIdReturnsSuccess()
    {
        Guid id = PlatformSeed.GetPlatforms().First().Id;

        var response = await _httpClient.GetAsync($"api/platforms/{id}");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetByIdWithInvalidIdReturnsNotFound()
    {
        using var client = _factory.CreateClient();

        var response = await client.GetAsync("api/platforms/-1");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    public void Dispose()
    {
        _factory.Dispose();
        _httpClient.Dispose();
        GC.SuppressFinalize(this);
    }
}