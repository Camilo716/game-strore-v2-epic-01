using GameStore.Api;

var builder = WebApplication.CreateBuilder(args);

Startup startup = new(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.ConfigureMiddlewares(app, app.Environment);

app.Run();

public partial class Program
{
}