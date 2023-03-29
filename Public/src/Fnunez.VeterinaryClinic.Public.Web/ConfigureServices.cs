using System.Reflection;
using Fnunez.VeterinaryClinic.Public.Web.Helpers.SymmetricEncryption;
using Fnunez.VeterinaryClinic.Public.Web.ServiceBus;
using Fnunez.VeterinaryClinic.Public.Web.ServiceBus.Observers;
using Fnunez.VeterinaryClinic.Public.Web.Services.Appointment;
using Fnunez.VeterinaryClinic.Public.Web.Services.Language;
using Fnunez.VeterinaryClinic.Public.Web.Settings;
using MassTransit;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllersWithViews();

        services.AddSingleton<IRabbitMqSetting>(configuration
            .GetSection(typeof(RabbitMqSetting).Name)
            .Get<RabbitMqSetting>()!);

        services.AddScoped<IServiceBus, MassTransitServiceBus>();

        services.AddConsumeObserver<LoggingConsumeObserver>();

        services.AddPublishObserver<LoggingPublishObserver>();

        services.AddReceiveObserver<LoggingReceiveObserver>();

        services.AddSendObserver<LoggingSendObserver>();

        services.AddMassTransit(mt =>
        {
            mt.AddConsumers(Assembly.GetExecutingAssembly());

            mt.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqSetting = context
                    .GetRequiredService<IRabbitMqSetting>();

                cfg.Host(rabbitMqSetting.HostAddress);

                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<IAppointmentService, AppointmentService>();

        services.AddScoped<ILanguageService, LanguageService>();

        services.AddSingleton<ISymmetricEncryptionSetting>(configuration
            .GetSection(typeof(SymmetricEncryptionSetting).Name)
            .Get<SymmetricEncryptionSetting>()!);

        services.AddSingleton<ISymmetricEncryptionHelper, SymmetricEncryptionHelper>();

        return services;
    }

    public static WebApplication AddWebApplicationBuilder(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        return app;
    }
}