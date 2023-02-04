using Fnunez.VeterinaryClinic.Identity.Infrastructure.Persistence.Contexts;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        return services;
    }

    public static WebApplication AddWebApplicationBuilder(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            Task.Run(() => SeedDataAsync(app));
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseIdentityServer();

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

            await configurationStoreDbSeeder.SeedDataAsync();

            var operationalStoreDbSeeder = scope.ServiceProvider
                .GetRequiredService<OperationalStoreDbContextSeeder>();

            await operationalStoreDbSeeder.MigrateAsync();
        }
    }
}