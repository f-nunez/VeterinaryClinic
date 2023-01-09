using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client;
using Radzen;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.TimeZone;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Settings;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.UserSettings;
using Blazored.LocalStorage;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.Spinner;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// register HttpClient and HttpService
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiUrl"]!) });
builder.Services.AddScoped<IHttpService, HttpService>();

// register Feature services
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAppointmentTypeService, AppointmentTypeService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClinicService, ClinicService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<ISpinnerService, SpinnerService>();
builder.Services.AddScoped<IUserSettingsService, UserSettingsService>();

// register Cookie settings
builder.Services.AddSingleton<ICookieSettings>(builder.Configuration.GetSection(typeof(CookieSettings).Name).Get<CookieSettings>()!);

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

await builder.Build().RunAsync();
