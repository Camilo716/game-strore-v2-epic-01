using GameStore.Core.Models;
using GameStore.Infraestructure.Data;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Infraestructure;

public class GenreRepositoryTests
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsGenresInDatabase()
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

    [Fact]
    public async Task Insert_GivenValidGenre_InsertsGenreInDatabase()
    {
        var dbContext = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var unitOfWork = new UnitOfWork(dbContext);
        var validGenre = new Genre() { Name = "Adventure" };

        await unitOfWork.GenreRepository.InsertAsync(validGenre);
        await unitOfWork.SaveChangesAsync();

        Assert.Equal(GenreSeed.GetGenres().Count + 1, dbContext.Genres.Count());
    }

    [Fact]
    public async Task Update_GivenValidGenre_UpdatesGenreInDatabase()
    {
        var dbContext = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var unitOfWork = new UnitOfWork(dbContext);
        var genre = GenreSeed.GetGenres().First();
        genre.Name = "Adventure";

        unitOfWork.GenreRepository.Update(genre);
        await unitOfWork.SaveChangesAsync();

        var updatedGenre = await dbContext.Genres.FindAsync(genre.Id);
        Assert.Equal(genre, updatedGenre);
    }
}