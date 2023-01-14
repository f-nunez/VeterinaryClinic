using System.Globalization;
using Blazored.LocalStorage;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Settings;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.Language;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.Spinner;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.TimeZone;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.UserSettings;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// register HttpClient and HttpService
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiUrl"]!) });
builder.Services.AddScoped<IHttpService, HttpService>();

// register Feature services
builder.Services.AddScoped<IAppointmentTypeService, AppointmentTypeService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClinicService, ClinicService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<IRoomService, RoomService>();

builder.Services.AddScoped<ISpinnerService, SpinnerService>();
builder.Services.AddScoped<IUserSettingsService, UserSettingsService>();

// register Cookie settings
builder.Services.AddSingleton<ICookieSettings>(builder.Configuration.GetSection(typeof(CookieSettings).Name).Get<CookieSettings>()!);

// register Language component
builder.Services.AddSingleton<ILanguageComponentData>(builder.Configuration.GetSection(typeof(LanguageComponentData).Name).Get<LanguageComponentData>()!);
builder.Services.AddScoped<ILanguageComponentService, LanguageComponentService>();

// register Spinner component
builder.Services.AddScoped<ISpinnerComponentService, SpinnerComponentService>();

// register TimeZone component
builder.Services.AddSingleton<ITimeZoneComponentData>(builder.Configuration.GetSection(typeof(TimeZoneComponentData).Name).Get<TimeZoneComponentData>()!);
builder.Services.AddScoped<ITimeZoneComponentService, TimeZoneComponentService>();

// register UserSettings component
builder.Services.AddScoped<IUserSettingsComponentService, UserSettingsComponentService>();

// register BlazoredLocalStorage
builder.Services.AddBlazoredLocalStorage();

// register Radzen services
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();

// register Language resources for Localizer
builder.Services.AddLocalization(options => { options.ResourcesPath = "Resources"; });

// load saved language culture
var host = builder.Build();
var userSettingsService = host.Services.GetRequiredService<IUserSettingsService>();
if (userSettingsService != null)
{
    string cultureCode = await userSettingsService.GetLanguageCultureCode();
    var cultureInfo = CultureInfo.CreateSpecificCulture(cultureCode);
    CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
    CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
}

await host.RunAsync();
