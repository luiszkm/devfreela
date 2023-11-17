using DevFreela.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersConfigurations(builder.Configuration);
builder.Services.AddAppConnections(builder.Configuration);
builder.Services.AddUseCases();

var app = builder.Build();

app.UseDocumentation();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
