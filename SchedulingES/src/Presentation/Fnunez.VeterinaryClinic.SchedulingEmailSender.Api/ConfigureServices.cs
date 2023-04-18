using Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Helpers.SymmetricEncryption;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Services.EmailTemplate;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Services.Language;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Services.StringRazorRender;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Settings;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.Language;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.StringRazorRender;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.HttpOverrides;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Needed when run behind a reverse proxy
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor
                | ForwardedHeaders.XForwardedProto;
        });

        services.AddRazorPages();

        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

        services.AddScoped<ILanguageService, LanguageService>();

        services.AddScoped<IStringRazorRenderService, StringRazorRenderService>();

        services.AddSingleton<IEmailTemplateSetting>(configuration
            .GetSection(typeof(EmailTemplateSetting).Name)
            .Get<EmailTemplateSetting>()!);

        services.AddSingleton<IEmailTemplateService, EmailTemplateService>();

        services.AddSingleton<ISymmetricEncryptionSetting>(configuration
            .GetSection(typeof(SymmetricEncryptionSetting).Name)
            .Get<SymmetricEncryptionSetting>()!);

        services.AddSingleton<ISymmetricEncryptionHelper, SymmetricEncryptionHelper>();

        return services;
    }

    public static WebApplication AddWebApplicationBuilder(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        switch (app.Environment.EnvironmentName)
        {
            case "DockerNginx":
                app.UseForwardedHeaders();
                app.UseSwagger();
                app.UseSwaggerUI();
                Task.Run(() => SeedDataAsync(app));
                break;
            case "DockerDevelopment":
            case "Development":
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseHsts();
                app.UseHttpsRedirection();
                Task.Run(() => SeedDataAsync(app));
                break;
            default:
                app.UseHsts();
                app.UseHttpsRedirection();
                break;
        }

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