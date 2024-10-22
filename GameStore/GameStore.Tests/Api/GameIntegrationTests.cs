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
}