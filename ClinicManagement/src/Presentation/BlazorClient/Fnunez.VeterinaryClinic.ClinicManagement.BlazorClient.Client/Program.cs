using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// register HttpClient and HttpService
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiUrl"]!) });
builder.Services.AddScoped<HttpService>();

// register http services
builder.Services.AddScoped<AppointmentTypeService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<RoomService>();

await builder.Build().RunAsync();
