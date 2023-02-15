using Blazored.LocalStorage;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.BackendForFrontend;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Handlers;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Settings;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.Language;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.Spinner;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.TimeZone;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.UserSettings;
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

        services.AddSingleton<IPhotoFileSetting>(
            configuration.GetSection(typeof(PhotoFileSetting).Name)
            .Get<PhotoFileSetting>()!);

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

        services.AddScoped<IHttpService, HttpService>();

        // register Feature services
        services.AddScoped<IAppointmentTypeService, AppointmentTypeService>();

        services.AddScoped<IClientService, ClientService>();

        services.AddScoped<IClinicService, ClinicService>();

        services.AddScoped<IDoctorService, DoctorService>();

        services.AddScoped<IPatientService, PatientService>();

        services.AddScoped<IRoomService, RoomService>();

        services.AddScoped<ISecurityService, SecurityService>();

        services.AddScoped<ISpinnerService, SpinnerService>();

        services.AddScoped<IUserSettingsService, UserSettingsService>();

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
            options.AddPolicy("RequiredWriterPolicy", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("Manager");
            });
        });

        return services;
    }
}