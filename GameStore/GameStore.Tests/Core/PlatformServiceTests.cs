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

        mockUnitOfWork.Setup(m => m.PlatformRepository.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(PlatformSeed.Platforms.First());

        var platformService = new PlatformService(mockUnitOfWork.Object);

        int id = 1;

        var platform = await platformService.GetByIdAsync(id);

        Assert.NotNull(platform);
        Assert.Equal(platform.Id, id);
    }
}