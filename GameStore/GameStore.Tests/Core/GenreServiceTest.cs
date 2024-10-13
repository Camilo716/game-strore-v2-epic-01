using GameStore.Core.Interfaces;
using GameStore.Core.Models;
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
        var genreService = new GenreService(unitOfWork.Object);

        var genres = await genreService.GetAllAsync();

        Assert.NotNull(genres);
        Assert.Equal(GenreSeed.GetGenres().Count, genres.Count());
    }

    [Fact]
    public async Task GetByParentId_GivenValidParentId_ReturnsChildrenGenres()
    {
        Mock<IUnitOfWork> unitOfWork = new();
        unitOfWork.Setup(m => m.GenreRepository.GetByParentIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) => GenreSeed.GetGenres().Where(g => g.ParentGenreId == id));

        var genreService = new GenreService(unitOfWork.Object);

        var childrenGenres = await genreService.GetByParentIdAsync(GenreSeed.Action.Id);

        Assert.NotNull(childrenGenres);
        Assert.Equivalent(new List<Genre>() { GenreSeed.Shooter }, childrenGenres);
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