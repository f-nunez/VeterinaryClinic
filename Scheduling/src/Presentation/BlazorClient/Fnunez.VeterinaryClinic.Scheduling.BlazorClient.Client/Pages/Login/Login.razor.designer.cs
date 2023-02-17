using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Login;

public partial class LoginComponent : ComponentBase
{
    [Inject]
    private NavigationManager _navigationManager { get; set; }

    [Inject]
    private ISecurityService _securityService { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    [Inject]
    protected IStringLocalizer<LoginComponent> StringLocalizer { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (_securityService.User != null)
            _navigationManager.NavigateTo("/", false);
    }

    protected void OnClickLogin()
    {
        _spinnerService.Show();
        _securityService.Login();
    }
}