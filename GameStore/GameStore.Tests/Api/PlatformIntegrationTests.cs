using System.Net;
using System.Net.Http.Json;
using GameStore.Api.Dtos.PlatformDtos;
using GameStore.Core.Models;
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

    [Fact]
    public async Task GetAll_WithoutPagination_ReturnsAllPlatforms()
    {
        var response = await HttpClient.GetAsync("api/platforms");

        response.EnsureSuccessStatusCode();
        var platforms = await HttpHelper.GetModelFromHttpResponseAsync<IEnumerable<Platform>>(response);
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
    public async Task Post_GivenValidPlatform_CreatesPlatform()
    {
        PlatformPostRequest validPlatform = new()
        {
            Platform = new SimplePlatformDto() { Type = "Virtual Reality" },
        };

        var response = await HttpClient.PostAsJsonAsync("api/platforms", validPlatform);

        response.EnsureSuccessStatusCode();
        Assert.Equal(PlatformSeed.GetPlatforms().Count + 1, DbContext.Platforms.Count());
    }

    [Fact]
    public async Task Put_GivenValidPlatform_UpdatesPlatform()
    {
        SimplePlatformWithIdDto platformDto = new()
        {
            Id = PlatformSeed.Mobile.Id,
            Type = "Virtual Reality",
        };
        PlatformPutRequest request = new()
        {
            Platform = platformDto,
        };

        var response = await HttpClient.PutAsJsonAsync("api/platforms", request);

        response.EnsureSuccessStatusCode();
    }

    [Theory]
    [MemberData(nameof(InvalidPlatformPostRequests))]
    public async Task Post_GivenInvalidPlatform_ReturnsBadRequest(PlatformPostRequest invalidPlatformRequest)
    {
        var response = await HttpClient.PostAsJsonAsync("api/platforms", invalidPlatformRequest);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public static IEnumerable<object[]> InvalidPlatformPostRequests() =>
        [
            [
                new PlatformPostRequest()
                {
                    Platform = null, // Without Platform
                },
            ],
            [
                new PlatformPostRequest()
                {
                    Platform = new SimplePlatformDto() { }, // Platform Without Type
                },
            ],
        ];
}