using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");

builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazorClientWebServices(builder.Configuration);

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
