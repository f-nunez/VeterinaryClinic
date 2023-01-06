using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client;
using Radzen;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// register HttpClient and HttpService
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiUrl"]!) });
builder.Services.AddScoped<HttpService>();

// register http services
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<AppointmentTypeService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<ClinicService>();
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<RoomService>();

// register Radzen services
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();

// register Spinner
builder.Services.AddScoped<LayoutSpinnerService>();

await builder.Build().RunAsync();
