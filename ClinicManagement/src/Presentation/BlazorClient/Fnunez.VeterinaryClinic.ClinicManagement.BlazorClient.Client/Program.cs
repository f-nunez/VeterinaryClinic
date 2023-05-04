using System.Globalization;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");

builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazorClientWebServices(builder.Configuration, builder.HostEnvironment.BaseAddress);

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
