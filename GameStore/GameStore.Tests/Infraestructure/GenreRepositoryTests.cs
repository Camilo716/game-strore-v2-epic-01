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
    public async Task GetAll_ReturnsPlatformsInDatabase()
    {
        var dbContext = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var unitOfWork = new UnitOfWork(dbContext);

        var genre = await unitOfWork.GenreRepository.GetAllAsync();

        Assert.NotNull(genre);
        Assert.Equal(GenreSeed.GetGenres().Count, genre.Count());
    }
}