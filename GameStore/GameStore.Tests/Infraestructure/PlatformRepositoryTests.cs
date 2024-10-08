using GameStore.Infraestructure.Data;
using GameStore.Infraestructure.Repositories;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Infraestructure;

public class PlatformRepositoryTests
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsPlatformInDatabase()
    {
        var dbContext = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var platformRepository = new PlatformRepository(dbContext);
        Guid id = PlatformSeed.GetPlatforms().First().Id;

        var platform = await platformRepository.GetByIdAsync(id);

        Assert.NotNull(platform);
        Assert.Equal(id, platform.Id);
    }

    [Fact]
    public async Task GetAll_ReturnsPlatformsInDatabase()
    {
        var dbContext = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var platformRepository = new PlatformRepository(dbContext);

        var platform = await platformRepository.GetAllAsync();

        Assert.NotNull(platform);
        Assert.Equal(platform.Count(), PlatformSeed.GetPlatforms().Count);
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesPlatformInDatabase()
    {
        var dbContext = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var unitOfWork = new UnitOfWork(dbContext);
        Guid id = PlatformSeed.GetPlatforms().First().Id;

        await unitOfWork.PlatformRepository.DeleteByIdAsync(id);
        await unitOfWork.SaveChangesAsync();

        Assert.Equal(dbContext.Platforms.Count(), PlatformSeed.GetPlatforms().Count - 1);
    }
}