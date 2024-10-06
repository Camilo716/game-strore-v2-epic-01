using GameStore.Infraestructure.Data;
using GameStore.Tests.Seed;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Tests.Api;

public class BaseIntegrationTest : IDisposable
{
    public BaseIntegrationTest()
    {
        Factory = new CustomWebApplicationFactory();
        HttpClient = Factory.CreateClient();

        using var scope = Factory.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<GameStoreDbContext>();
        DbSeeder.SeedData(context);

        DbContext = context;
    }

    protected CustomWebApplicationFactory Factory { get; }

    protected HttpClient HttpClient { get; }

    protected GameStoreDbContext DbContext { get; }

    public void Dispose()
    {
        Factory.Dispose();
        HttpClient.Dispose();
        DbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}