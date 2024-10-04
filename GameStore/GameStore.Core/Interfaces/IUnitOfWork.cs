namespace GameStore.Core.Interfaces;

public interface IUnitOfWork
{
    public IPlatformRepository PlatformRepository { get; }
}