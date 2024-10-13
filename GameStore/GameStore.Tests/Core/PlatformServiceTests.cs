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
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var platformService = new PlatformService(unitOfWork.Object);
        Guid id = PlatformSeed.GetPlatforms().First().Id;

        var platform = await platformService.GetByIdAsync(id);

        Assert.NotNull(platform);
        Assert.Equal(platform.Id, id);
    }

    [Fact]
    public async Task GetAll_ReturnsPlatformsModel()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var platformService = new PlatformService(unitOfWork.Object);

        var platforms = await platformService.GetAllAsync();

        Assert.NotNull(platforms);
        Assert.Equal(platforms.Count(), PlatformSeed.GetPlatforms().Count);
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesPlatform()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        Guid id = PlatformSeed.GetPlatforms().First().Id;
        var platformService = new PlatformService(unitOfWork.Object);

        await platformService.DeleteAsync(id);

        unitOfWork.Verify(m => m.PlatformRepository.DeleteByIdAsync(id), Times.Once());
    }

    [Fact]
    public async Task Create_GivenValidPlatform_CreatesPlatform()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var validPlatform = new Platform() { Type = "Virtual Reality" };
        var platformService = new PlatformService(unitOfWork.Object);

        await platformService.CreateAsync(validPlatform);

        unitOfWork.Verify(
            m => m.PlatformRepository.InsertAsync(It.Is<Platform>(p => p.Type == validPlatform.Type)),
            Times.Once());
    }

    [Fact]
    public async Task Update_GivenValidPlatform_UpdatesPlatform()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var platform = new Platform() { Type = "Virtual Reality" };
        var platformService = new PlatformService(unitOfWork.Object);

        await platformService.UpdateAsync(platform);

        unitOfWork.Verify(
            m => m.PlatformRepository.Update(It.Is<Platform>(p => p.Type == platform.Type)),
            Times.Once());
    }

    private static Mock<IUnitOfWork> GetDummyUnitOfWorkMock()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork.Setup(m => m.PlatformRepository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(PlatformSeed.GetPlatforms().First());

        mockUnitOfWork.Setup(m => m.PlatformRepository.GetAllAsync())
            .ReturnsAsync(PlatformSeed.GetPlatforms());

        mockUnitOfWork.Setup(m => m.PlatformRepository.DeleteByIdAsync(It.IsAny<Guid>()));

        mockUnitOfWork.Setup(m => m.PlatformRepository.InsertAsync(It.IsAny<Platform>()));

        mockUnitOfWork.Setup(m => m.PlatformRepository.Update(It.IsAny<Platform>()));

        return mockUnitOfWork;
    }
}