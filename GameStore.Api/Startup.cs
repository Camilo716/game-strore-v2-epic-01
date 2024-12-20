using System.Text.Json.Serialization;
using AutoMapper;
using GameStore.Core.Interfaces;
using GameStore.Core.Services;
using GameStore.Infraestructure.Data;

namespace GameStore.Api;
public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration => configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        Infraestructure.Dependences.ConfigureServices(Configuration, services);

        services.AddSwaggerGen();
        services.AddAutoMapper(typeof(AutoMapperProfile));

        services.AddScoped<IPlatformService, PlatformService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IGameFileService, GameTextFileService>();

        services.AddControllers()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlerMiddleware>();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}