using GameStore.Api;
using GameStore.Infraestructure.Data;
using GameStore.Tests.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Tests.Api;

public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            RemoveDbContextServiceRegistration(services);

            services.AddDbContext<GameStoreContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            });

            SeedData(services);
        });
    }

    private static void RemoveDbContextServiceRegistration(IServiceCollection services)
    {
        var dbContextDescriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(GameStoreContext));

        if (dbContextDescriptor is not null)
        {
            services.Remove(dbContextDescriptor);
        }
    }

    private static void SeedData(IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        DbSeeder.SeedData(context);
    }
}