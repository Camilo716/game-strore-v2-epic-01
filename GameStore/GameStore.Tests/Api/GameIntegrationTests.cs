using System.Net;
using System.Net.Http.Json;
using GameStore.Api.Dtos;
using GameStore.Core.Models;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Api;

public class GameIntegrationTests : BaseIntegrationTest
{
    [Fact]
    public async Task GetAll_WithoutPagination_ReturnsAllGames()
    {
        var response = await HttpClient.GetAsync("api/games");

        response.EnsureSuccessStatusCode();
        var games = await HttpHelper.GetModelFromHttpResponseAsync<IEnumerable<Game>>(response);
        Assert.NotNull(games);
        Assert.Equal(games.Count(), GameSeed.GetGames().Count);
    }

    [Fact]
    public async Task GetGenresByGameKey_GivenValidKey_ReturnsSuccess()
    {
        const string gameKey = "gearsOfWar";

        var response = await HttpClient.GetAsync($"api/games/{gameKey}/genres");

        response.EnsureSuccessStatusCode();
        var genres = await HttpHelper.GetModelFromHttpResponseAsync<IEnumerable<Genre>>(response);
        Assert.NotNull(genres);
        Assert.All(
            genres,
            genre => Assert.Contains(
                gameKey,
                genre.Games.Select(game => game.Key)));
    }

    [Fact]
    public async Task GetPlatformsByGameKey_GivenValidKey_ReturnsSuccess()
    {
        const string gameKey = "gearsOfWar";

        var response = await HttpClient.GetAsync($"api/games/{gameKey}/platforms");

        response.EnsureSuccessStatusCode();
        var platforms = await HttpHelper.GetModelFromHttpResponseAsync<IEnumerable<Platform>>(response);
        Assert.NotNull(platforms);
        Assert.All(
            platforms,
            platform => Assert.Contains(
                gameKey,
                platform.Games.Select(game => game.Key)));
    }

    [Fact]
    public async Task Post_GivenValidGame_CreatesGame()
    {
        GameCreationDto game = new()
        {
            Game = new Game() { Name = "Halo3", Key = "Halo3" },
            GenresIds =
            [
                GenreSeed.Action.Id,
            ],
            PlatformsIds =
            [
                PlatformSeed.Console.Id,
            ],
        };

        var response = await HttpClient.PostAsJsonAsync("api/games", game);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(GameSeed.GetGames().Count + 1, DbContext.Games.Count());
    }
}