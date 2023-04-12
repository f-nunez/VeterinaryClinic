using Fnunez.VeterinaryClinic.Identity.Api.Settings;
using Fnunez.VeterinaryClinic.Identity.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.HttpOverrides;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var cookiePolicySetting = configuration
            .GetSection(typeof(CookiePolicySetting).Name)
            .Get<CookiePolicySetting>()!;

        // Needed when run behind a reverse proxy
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor
                | ForwardedHeaders.XForwardedProto;
        });

        services.Configure<CookiePolicyOptions>(options =>
        {
            options.MinimumSameSitePolicy = cookiePolicySetting.MinimumSameSitePolicy;
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        return services;
    }

    public static WebApplication AddWebApplicationBuilder(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        switch (app.Environment.EnvironmentName)
        {
            case "DockerNginx":
                app.UseDeveloperExceptionPage();
                app.UseForwardedHeaders();
                Task.Run(() => SeedDataAsync(app));
                break;
            case "DockerDevelopment":
            case "Development":
                app.UseDeveloperExceptionPage();
                app.UseHsts();
                app.UseHttpsRedirection();
                Task.Run(() => SeedDataAsync(app));
                break;
            default:
                app.UseHsts();
                app.UseHttpsRedirection();
                break;
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseIdentityServer();

        app.UseCookiePolicy();

        app.UseAuthorization();

        app.MapRazorPages().RequireAuthorization();

        app.MapControllers();

        return app;
    }

    private static async void SeedDataAsync(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var applicationDbSeeder = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContextSeeder>();

            await applicationDbSeeder.MigrateAsync();

            await applicationDbSeeder.SeedDataAsync();

            var configurationStoreDbSeeder = scope.ServiceProvider
                .GetRequiredService<ConfigurationStoreDbContextSeeder>();

            await configurationStoreDbSeeder.MigrateAsync();

            await configurationStoreDbSeeder.SeedDataAsync(
                app.Environment.EnvironmentName);

            var operationalStoreDbSeeder = scope.ServiceProvider
                .GetRequiredService<OperationalStoreDbContextSeeder>();

            await operationalStoreDbSeeder.MigrateAsync();
        }
    }
}