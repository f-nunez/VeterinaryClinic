using Blazored.LocalStorage;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.BackendForFrontend;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Handlers;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Settings;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification.Factories;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.Language;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.Spinner;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.TimeZone;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.UserSettings;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddBlazorClientWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // register Settings
        services.AddSingleton<IBackendForFrontendSetting>(
            configuration.GetSection(typeof(BackendForFrontendSetting).Name)
            .Get<BackendForFrontendSetting>()!);

        services.AddSingleton<ICookieSetting>(
            configuration.GetSection(typeof(CookieSetting).Name)
            .Get<CookieSetting>()!);

        // authentication state plumbing
        services.AddScoped<AuthenticationStateProvider, BffAuthenticationStateProvider>();

        services.AddTransient<AntiforgeryHandler>();

        // register HttpClient and HttpService
        services.AddHttpClient("backendForFrontend", client =>
            client.BaseAddress = new Uri(configuration["ApplicationUrl"]!))
                .AddHttpMessageHandler<AntiforgeryHandler>();

        services.AddTransient(sp =>
            sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("backendForFrontend"));

        services.AddScoped<ISchedulingApiHttpService, SchedulingApiHttpService>();

        services.AddScoped<ISchedulingNotificationsApiHttpService, SchedulingNotificationsApiHttpService>();

        // register Feature services
        services.AddScoped<IAppNotificationService, AppNotificationService>();

        services.AddScoped<IAppointmentService, AppointmentService>();

        services.AddScoped<IAppointmentTypeService, AppointmentTypeService>();

        services.AddScoped<IClientService, ClientService>();

        services.AddScoped<IClinicService, ClinicService>();

        services.AddScoped<IDoctorService, DoctorService>();

        services.AddScoped<IPatientService, PatientService>();

        services.AddScoped<IRoomService, RoomService>();

        services.AddScoped<ISecurityService, SecurityService>();

        services.AddScoped<ISpinnerService, SpinnerService>();

        services.AddScoped<IUserSettingsService, UserSettingsService>();

        // register AppNotification component
        services.AddScoped<IAppNotificationBuilder, AppNotificationBuilder>();
        services.AddScoped<IAppNotificationComponentService, AppNotificationComponentService>();

        // register Language component
        services.AddSingleton<ILanguageComponentData>(
            configuration.GetSection(typeof(LanguageComponentData).Name)
            .Get<LanguageComponentData>()!);

        services.AddScoped<ILanguageComponentService, LanguageComponentService>();

        // register Spinner component
        services.AddScoped<ISpinnerComponentService, SpinnerComponentService>();

        // register TimeZone component
        services.AddSingleton<ITimeZoneComponentData>(
            configuration.GetSection(typeof(TimeZoneComponentData).Name)
            .Get<TimeZoneComponentData>()!);

        services.AddScoped<ITimeZoneComponentService, TimeZoneComponentService>();

        // register UserSettings component
        services.AddScoped<IUserSettingsComponentService, UserSettingsComponentService>();

        // register BlazoredLocalStorage
        services.AddBlazoredLocalStorage();

        // register Radzen services
        services.AddScoped<DialogService>();

        services.AddScoped<NotificationService>();

        // register Language resources for Localizer
        services.AddLocalization(options => { options.ResourcesPath = "Resources"; });

        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("RequiredReaderPolicy", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("Manager", "Staff");
            });

            options.AddPolicy("RequiredWriterPolicy", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("Manager", "Staff");
            });
        });

        return services;
    }
}