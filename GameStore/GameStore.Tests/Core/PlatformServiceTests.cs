using GameStore.Core.Interfaces;
using GameStore.Core.Services;
using GameStore.Tests.Seed;
using Moq;

namespace GameStore.Tests.Core;

public class PlatformServiceTests
{
    [Fact]
    public async Task GetByIdReturnsPlatformModel()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork.Setup(m => m.PlatformRepository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(PlatformSeed.GetPlatforms().First());

        var platformService = new PlatformService(mockUnitOfWork.Object);

        Guid id = PlatformSeed.GetPlatforms().First().Id;

        var platform = await platformService.GetByIdAsync(id);

        Assert.NotNull(platform);
        Assert.Equal(platform.Id, id);
    }
}