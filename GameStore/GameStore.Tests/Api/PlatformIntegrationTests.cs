using System.Net;
using GameStore.Core.Models;
using GameStore.Tests.Seed;
using Newtonsoft.Json;

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

    [Fact]
    public async Task GetAll_WithoutPagination_ReturnsAllPlatforms()
    {
        var response = await HttpClient.GetAsync("api/platforms");

        response.EnsureSuccessStatusCode();
        var platforms = await GetModelFromHttpResponse<IEnumerable<Platform>>(response);
        Assert.NotNull(platforms);
        Assert.Equal(platforms.Count(), PlatformSeed.GetPlatforms().Count);
    }

    private static async Task<T> GetModelFromHttpResponse<T>(HttpResponseMessage response)
    {
        string stringResponse = await response.Content.ReadAsStringAsync();
        var platforms = JsonConvert.DeserializeObject<T>(stringResponse);
        return platforms;
    }
}