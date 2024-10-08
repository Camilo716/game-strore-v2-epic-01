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

    [Fact]
    public async Task Delete_GivenValidId_DeletesPlatform()
    {
        Guid id = PlatformSeed.GetPlatforms().First().Id;

        var response = await HttpClient.DeleteAsync($"api/platforms/{id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(DbContext.Platforms.Count(), PlatformSeed.GetPlatforms().Count - 1);
    }

    private static async Task<T> GetModelFromHttpResponse<T>(HttpResponseMessage response)
    {
        string stringResponse = await response.Content.ReadAsStringAsync();
        var platforms = JsonConvert.DeserializeObject<T>(stringResponse);
        return platforms;
    }
}