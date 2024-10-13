using System.Net;
using System.Net.Http.Json;
using GameStore.Api.Dtos;
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
        Assert.Equal(PlatformSeed.GetPlatforms().Count - 1, DbContext.Platforms.Count());
    }

    [Fact]
    public async Task Post_GivenValidPlatform_CreatePlatform()
    {
        var validPlatform = new PlatformCreationDto()
        {
            Platform = new Platform() { Type = "Virtual Reality" },
        };

        var response = await HttpClient.PostAsJsonAsync("api/platforms", validPlatform);

        response.EnsureSuccessStatusCode();
        Assert.Equal(PlatformSeed.GetPlatforms().Count + 1, DbContext.Platforms.Count());
    }

    [Fact]
    public async Task Put_GivenValidPlatform_UpdatesPlatform()
    {
        var platform = PlatformSeed.GetPlatforms().First();
        platform.Type = "Virtual Reality";
        var request = new PlatformCreationDto()
        {
            Platform = platform,
        };

        var response = await HttpClient.PutAsJsonAsync("api/platforms", request);

        response.EnsureSuccessStatusCode();
    }

    private static async Task<T> GetModelFromHttpResponse<T>(HttpResponseMessage response)
    {
        string stringResponse = await response.Content.ReadAsStringAsync();
        var platforms = JsonConvert.DeserializeObject<T>(stringResponse);
        return platforms;
    }
}