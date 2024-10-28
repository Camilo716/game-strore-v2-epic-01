using System.Net;
using System.Net.Http.Json;
using GameStore.Api.Dtos.GameDtos;
using GameStore.Core.Models;
using GameStore.Tests.Api.ClassData;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Api;

public class GameIntegrationTests : BaseIntegrationTest
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsSuccess()
    {
        Guid id = GameSeed.GearsOfWar.Id;

        var response = await HttpClient.GetAsync($"api/games/find/{id}");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetByKey_GivenValidKey_ReturnsSuccess()
    {
        string key = GameSeed.GearsOfWar.Key;

        var response = await HttpClient.GetAsync($"api/games/{key}");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetAll_WithoutPagination_ReturnsAllGames()
    {
        var response = await HttpClient.GetAsync("api/games");

        response.EnsureSuccessStatusCode();
        var games = await HttpHelper.GetModelFromHttpResponseAsync<IEnumerable<Game>>(response);
        Assert.NotNull(games);
        Assert.Equal(GameSeed.GetGames().Count, games.Count());
    }

    [Fact]
    public async Task GetGenresByGameKey_GivenValidKey_ReturnsSuccess()
    {
        string gameKey = GameSeed.GearsOfWar.Key;

        var response = await HttpClient.GetAsync($"api/games/{gameKey}/genres");

        response.EnsureSuccessStatusCode();
        var genres = await HttpHelper.GetModelFromHttpResponseAsync<IEnumerable<Genre>>(response);
        Assert.NotNull(genres);
        var expectedGenres = DbContext.Genres
            .Where(genre => genre.Games.Select(g => g.Key).Contains(gameKey));
        Assert.Equal(expectedGenres.Count(), genres.Count());
    }

    [Fact]
    public async Task GetPlatformsByGameKey_GivenValidKey_ReturnsSuccess()
    {
        string gameKey = GameSeed.GearsOfWar.Key;

        var response = await HttpClient.GetAsync($"api/games/{gameKey}/platforms");

        response.EnsureSuccessStatusCode();
        var platforms = await HttpHelper.GetModelFromHttpResponseAsync<IEnumerable<Platform>>(response);
        Assert.NotNull(platforms);
        var expectedPlatforms = DbContext.Platforms
            .Where(platform => platform.Games.Select(g => g.Key).Contains(gameKey));
        Assert.Equal(expectedPlatforms.Count(), platforms.Count());
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesgGame()
    {
        Guid id = GameSeed.GearsOfWar.Id;

        var response = await HttpClient.DeleteAsync($"api/games/{id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(GameSeed.GetGames().Count - 1, DbContext.Games.Count());
    }

    [Fact]
    public async Task Post_GivenValidGame_CreatesGame()
    {
        GamePostRequest game = new()
        {
            Game = new SimpleGameDto() { Name = "Halo3", Key = "Halo3" },
            Genres =
            [
                GenreSeed.Action.Id,
            ],
            Platforms =
            [
                PlatformSeed.Console.Id,
            ],
        };

        var response = await HttpClient.PostAsJsonAsync("api/games", game);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(GameSeed.GetGames().Count + 1, DbContext.Games.Count());
    }

    [Fact]
    public async Task Put_GivenValidGame_UpdatesGame()
    {
        SimpleGameWithIdDto gameDto = new()
        {
            Id = GameSeed.GearsOfWar.Id,
            Name = "Gears of War Edited",
            Key = GameSeed.GearsOfWar.Key,
            Description = GameSeed.GearsOfWar.Description,
        };
        GamePutRequest request = new()
        {
            Game = gameDto,
            Genres =
            [
                GenreSeed.Shooter.Id,
            ],
            Platforms =
            [
                PlatformSeed.Mobile.Id
            ],
        };

        var response = await HttpClient.PutAsJsonAsync("api/games", request);

        response.EnsureSuccessStatusCode();
    }

    [Theory]
    [ClassData(typeof(InvalidGamesPostRequestTestData))]
    public async Task Post_GivenInvalidGame_ReturnsBadRequest(GamePostRequest invalidGameRequest)
    {
        var response = await HttpClient.PostAsJsonAsync("api/games", invalidGameRequest);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [ClassData(typeof(InvalidGamesPutRequestTestData))]
    public async Task Put_GivenInvalidGame_ReturnsBadRequest(GamePutRequest invalidGameRequest)
    {
        var response = await HttpClient.PutAsJsonAsync("api/games", invalidGameRequest);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}