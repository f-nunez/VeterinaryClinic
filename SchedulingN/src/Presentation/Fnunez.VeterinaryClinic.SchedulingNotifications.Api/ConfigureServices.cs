using Fnunez.VeterinaryClinic.SchedulingNotifications.Api.Filters;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Api.Services;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddControllers(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>());

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

        return services;
    }

    public static WebApplication AddWebApplicationBuilder(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI();

            Task.Run(() => SeedDataAsync(app));
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseHealthChecks("/api/health");

        return app;
    }

    private static async void SeedDataAsync(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var seeder = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContextSeeder>();

            await seeder.MigrateAsync();

            await seeder.SeedDataAsync();
        }
    }
}