using System.Net;
using System.Net.Http.Json;
using GameStore.Api.Dtos.GenresDtos;
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

    [Fact]
    public async Task Delete_GivenValidId_DeletesGenre()
    {
        Guid id = GenreSeed.GetGenres().First().Id;

        var response = await HttpClient.DeleteAsync($"api/genres/{id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(GenreSeed.GetGenres().Count - 1, DbContext.Genres.Count());
    }

    [Fact]
    public async Task Post_GivenValidGenre_CreatesGenre()
    {
        var genre = new GenrePostRequest()
        {
            Genre = new SimpleGenreDto() { Name = "Adventure" },
        };

        var response = await HttpClient.PostAsJsonAsync("api/genres", genre);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(GenreSeed.GetGenres().Count + 1, DbContext.Genres.Count());
    }

    [Fact]
    public async Task Put_GivenValidGenre_UpdatesGenre()
    {
        var genre = GenreSeed.GetGenres().First();
        SimpleGenreWithIdDto genreDto = new()
        {
            Id = genre.Id,
            Name = "Adventure",
            ParentGenreId = genre.ParentGenreId,
        };
        GenrePutRequest request = new()
        {
            Genre = genreDto,
        };

        var response = await HttpClient.PutAsJsonAsync("api/genres", request);

        response.EnsureSuccessStatusCode();
    }
}