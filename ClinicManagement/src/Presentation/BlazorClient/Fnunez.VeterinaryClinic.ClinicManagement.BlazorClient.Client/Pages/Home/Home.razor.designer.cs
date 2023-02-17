using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Home;

public partial class HomeComponent : ComponentBase
{
    [Inject]
    private ISecurityService _securityService { get; set; }

    protected string NameOfUser { get; set; }

    protected async override Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        
        if (_securityService.User != null)
            NameOfUser = _securityService.User.Name;
    }
}