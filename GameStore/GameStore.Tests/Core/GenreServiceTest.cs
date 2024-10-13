using GameStore.Core.Interfaces;
using GameStore.Core.Services;
using GameStore.Tests.Seed;
using Moq;

namespace GameStore.Tests.Core;

public class GenreServiceTest
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsPlatformModel()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var genreService = new GenreService(unitOfWork.Object);
        Guid id = GenreSeed.GetGenres().First().Id;

        var platform = await genreService.GetByIdAsync(id);

        Assert.NotNull(platform);
        Assert.Equal(platform.Id, id);
    }

    private static Mock<IUnitOfWork> GetDummyUnitOfWorkMock()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork.Setup(m => m.GenreRepository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(GenreSeed.GetGenres().First());

        return mockUnitOfWork;
    }
}