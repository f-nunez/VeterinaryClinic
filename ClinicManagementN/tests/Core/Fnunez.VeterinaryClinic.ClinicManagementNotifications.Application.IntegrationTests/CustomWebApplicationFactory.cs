using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.IntegrationTests;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private static string UserIdAsManager = "9f79b45e-1ebe-4bb2-9d6f-e00da51b0848";
    private string _userId;

    public CustomWebApplicationFactory()
    {
        _userId = UserIdAsManager;
    }

    public CustomWebApplicationFactory(string userId)
    {
        if (!string.IsNullOrEmpty(userId))
            _userId = userId;
        else
            _userId = UserIdAsManager;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.ConfigureServices((builder, services) =>
        {
            services
                .Remove<ICurrentUserService>()
                .AddTransient(provider => Mock.Of<ICurrentUserService>(s =>
                    s.UserId == _userId));

            services
                .Remove<DbContextOptions<ApplicationDbContext>>()
                .AddDbContext<ApplicationDbContext>((sp, options) =>
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("DefaultConnection"),
                        builder => builder.MigrationsAssembly(
                            typeof(ApplicationDbContext).Assembly.FullName
                        )
                    )
                );
        });
    }
}