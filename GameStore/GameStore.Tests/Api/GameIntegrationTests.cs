using GameStore.Core.Models;

namespace GameStore.Tests.Api;

public class GameIntegrationTests : BaseIntegrationTest
{
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
}