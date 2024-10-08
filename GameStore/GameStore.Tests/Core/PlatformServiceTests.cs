using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Core.Services;
using GameStore.Tests.Seed;
using Moq;

namespace GameStore.Tests.Core;

public class PlatformServiceTests
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsPlatformModel()
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

    [Fact]
    public async Task GetAll_ReturnsPlatformsModel()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork.Setup(m => m.PlatformRepository.GetAllAsync())
            .ReturnsAsync(PlatformSeed.GetPlatforms());

        var platformService = new PlatformService(mockUnitOfWork.Object);

        var platforms = await platformService.GetAllAsync();

        Assert.NotNull(platforms);
        Assert.Equal(platforms.Count(), PlatformSeed.GetPlatforms().Count);
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesPlatform()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.PlatformRepository.DeleteByIdAsync(It.IsAny<Guid>()));

        Guid id = PlatformSeed.GetPlatforms().First().Id;

        var platformService = new PlatformService(mockUnitOfWork.Object);

        await platformService.DeleteAsync(id);

        mockUnitOfWork.Verify(m => m.PlatformRepository.DeleteByIdAsync(id), Times.Once());
    }

    [Fact]
    public async Task Create_GivenValidPlatform_CreatesPlatform()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.PlatformRepository.InsertAsync(It.IsAny<Platform>()));
        var validPlatform = new Platform() { Type = "Virtual Reality" };

        var platformService = new PlatformService(mockUnitOfWork.Object);

        await platformService.CreateAsync(validPlatform);

        mockUnitOfWork.Verify(
            m => m.PlatformRepository.InsertAsync(It.Is<Platform>(p => p.Type == validPlatform.Type)),
            Times.Once());
    }
}