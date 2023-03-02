using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Api.Filters;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Api.Services;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Api.Settings;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var corsPolicySetting = configuration
            .GetSection(typeof(CorsPolicySetting).Name)
            .Get<CorsPolicySetting>()!;

        services.AddHttpContextAccessor();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddCors(corsOptions =>
        {
            corsOptions.AddPolicy(typeof(CorsPolicySetting).Name, corsPolicyBuilder =>
            {
                corsPolicyBuilder.AllowAnyHeader();

                corsPolicyBuilder.AllowAnyMethod();

                corsPolicyBuilder.WithOrigins(
                    corsPolicySetting.BlazorServerUrl,
                    corsPolicySetting.IdentityServerUrl
                );
            });
        });

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

        app.UseRouting();

        app.UseCors(typeof(CorsPolicySetting).Name);

        app.UseAuthorization();

        app.UseHealthChecks("/api/health");

        app.MapControllers();

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