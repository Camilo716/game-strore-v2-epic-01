using GameStore.Core.Interfaces;
using GameStore.Core.Services;
using GameStore.Infraestructure.Data;

namespace GameStore.Api;
public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        Infraestructure.Dependences.ConfigureServices(Configuration, services);

        services.AddSwaggerGen();

        services.AddScoped<IPlatformService, PlatformService>();
        services.AddScoped<IGenreService, GenreService>();

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}