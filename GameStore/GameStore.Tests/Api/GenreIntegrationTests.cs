using GameStore.Core.Models;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Api;

public class GenreIntegrationTests : BaseIntegrationTest
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsSuccess()
    {
        Guid id = GenreSeed.GetGenres().First().Id;

        var response = await HttpClient.GetAsync($"api/genres/{id}");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetAll_WithoutPagination_ReturnsAllGenres()
    {
        var response = await HttpClient.GetAsync("api/genres");

        response.EnsureSuccessStatusCode();
        var genres = await HttpHelper.GetModelFromHttpResponseAsync<IEnumerable<Genre>>(response);
        Assert.NotNull(genres);
        Assert.Equal(genres.Count(), GenreSeed.GetGenres().Count);
    }

    [Fact]
    public async Task GetByParentId_GivenValidParentId_ReturnsSuccess()
    {
        Guid parentId = GenreSeed.Action.Id;

        var response = await HttpClient.GetAsync($"api/genres/{parentId}/genres");

        response.EnsureSuccessStatusCode();
    }
}