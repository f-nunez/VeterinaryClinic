using Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Helpers.SymmetricEncryption;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Services.StringRazorRender;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Settings;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.StringRazorRender;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Infrastructure.Persistence.Contexts;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddRazorPages();

        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

        services.AddScoped<IStringRazorRenderService, StringRazorRenderService>();

        services.AddSingleton<ISymmetricEncryptionSetting>(configuration
            .GetSection(typeof(SymmetricEncryptionSetting).Name)
            .Get<SymmetricEncryptionSetting>()!);

        services.AddSingleton<ISymmetricEncryptionHelper, SymmetricEncryptionHelper>();

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
        }
    }
}