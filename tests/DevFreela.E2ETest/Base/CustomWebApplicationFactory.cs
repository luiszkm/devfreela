using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.E2ETest.Base;

public class CustomWebApplicationFactory<TStartup>
     : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        /*builder.UseEnvironment("E2ETest");
        builder.ConfigureServices(service =>
        {
            var serviceProvider = service.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider
                .GetService<DevFreelaDbContext>();
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        });
        base.ConfigureWebHost(builder);*/


        // InMemory
        builder.ConfigureServices(service =>
        {
            var dboptions = service.FirstOrDefault(
                x => x.ServiceType ==
                     typeof(DbContextOptions<DevFreelaDbContext>));
            if (dboptions != null)
                service.Remove(dboptions);

            service.AddDbContext<DevFreelaDbContext>(
                options => options.UseInMemoryDatabase("DevFreela-In-Memory"));
        });
        base.ConfigureWebHost(builder);
    }

}
