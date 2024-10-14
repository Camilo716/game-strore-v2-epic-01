using GameStore.Core.Models;
using GameStore.Infraestructure.Data;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Infraestructure;

public class GenreRepositoryTests
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsPlatformsInDatabase()
    {
        var dbContext = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var unitOfWork = new UnitOfWork(dbContext);
        Guid id = GenreSeed.GetGenres().First().Id;

        var genre = await unitOfWork.GenreRepository.GetByIdAsync(id);

        Assert.NotNull(genre);
        Assert.Equal(id, genre.Id);
    }

    [Fact]
    public async Task GetAll_ReturnsGenresInDatabase()
    {
        var dbContext = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var unitOfWork = new UnitOfWork(dbContext);

        var genre = await unitOfWork.GenreRepository.GetAllAsync();

        Assert.NotNull(genre);
        Assert.Equal(GenreSeed.GetGenres().Count, genre.Count());
    }

    [Fact]
    public async Task GetByParentId_GivenValidParentId_ReturnsChildrenGenresInDatabase()
    {
        var dbContext = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var unitOfWork = new UnitOfWork(dbContext);

        var childrenGenres = await unitOfWork.GenreRepository.GetByParentIdAsync(GenreSeed.Action.Id);

        var expectedChildrenGenres = new List<Genre>() { GenreSeed.Shooter };
        Assert.Equivalent(expectedChildrenGenres, childrenGenres);
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesGenreInDatabase()
    {
        var dbContext = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var unitOfWork = new UnitOfWork(dbContext);
        Guid id = GenreSeed.GetGenres().First().Id;

        await unitOfWork.GenreRepository.DeleteByIdAsync(id);
        await unitOfWork.SaveChangesAsync();

        var expectedGenresInDb = GenreSeed.GetGenres().Count - 1;
        Assert.Equal(expectedGenresInDb, dbContext.Genres.Count());
    }
}