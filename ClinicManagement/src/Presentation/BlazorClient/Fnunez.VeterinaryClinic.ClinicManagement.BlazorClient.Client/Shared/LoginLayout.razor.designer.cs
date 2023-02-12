using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared;

public partial class LoginLayoutComponent : LayoutComponentBase
{
    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Inject]
    protected ISecurityService SecurityService { get; set; }

    [Inject]
    protected IStringLocalizer<LoginLayout> StringLocalizer { get; set; }

    protected RadzenBody RadzenBody;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (SecurityService.User is null)
            NavigationManager.NavigateTo("login", false);
    }
}