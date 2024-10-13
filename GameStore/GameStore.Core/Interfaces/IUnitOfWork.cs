namespace GameStore.Core.Interfaces;

public interface IUnitOfWork
{
    public IPlatformRepository PlatformRepository { get; }

    public IGenreRepository GenreRepository { get; }

    Task SaveChangesAsync();
}