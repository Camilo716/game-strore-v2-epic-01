using GameStore.Infraestructure.Data;
using GameStore.Infraestructure.Repositories;

namespace GameStore.Tests.Infraestructure;

public class PlatformRepositoryTests
{
    [Fact]
    public async Task PlatformRepository_GetById_ReturnsSingleValue()
    {
        var dbContext = new GameStoreContext(UnitTestHelper.GetUnitTestDbOptions());
        var platformRepository = new PlatformRepository(dbContext);

        var platform = await platformRepository.GetByIdAsync(1);

        Assert.NotNull(platform);
        Assert.Equal(1, platform.Id);
    }
}