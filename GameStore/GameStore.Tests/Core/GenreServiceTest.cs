using GameStore.Core.Interfaces;
using GameStore.Core.Services;
using GameStore.Tests.Seed;
using Moq;

namespace GameStore.Tests.Core;

public class GenreServiceTest
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsGenreModel()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var genreService = new GenreService(unitOfWork.Object);
        Guid id = GenreSeed.Action.Id;

        var genre = await genreService.GetByIdAsync(id);

        Assert.NotNull(genre);
        Assert.Equal(genre.Id, id);
    }

    [Fact]
    public async Task GetAll_ReturnsGenreModels()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var platformService = new GenreService(unitOfWork.Object);

        var genres = await platformService.GetAllAsync();

        Assert.NotNull(genres);
        Assert.Equal(GenreSeed.GetGenres().Count, genres.Count());
    }

    private static Mock<IUnitOfWork> GetDummyUnitOfWorkMock()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork.Setup(m => m.GenreRepository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(GenreSeed.Action);

        mockUnitOfWork.Setup(m => m.GenreRepository.GetAllAsync())
            .ReturnsAsync(GenreSeed.GetGenres());

        return mockUnitOfWork;
    }
}