using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Login;

public partial class LoginComponent : ComponentBase
{
    [Inject]
    private NavigationManager _navigationManager { get; set; }

    [Inject]
    private ISecurityService _securityService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (_securityService.User != null)
            _navigationManager.NavigateTo("/", false);
    }

    protected void OnClickLogin()
    {
        _securityService.Login();
    }
}