using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Clients;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Clients;

public partial class ClientDetailComponent : ComponentBase
{
    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    protected IStringLocalizer<ClientDetailComponent> StringLocalizer { get; set; }

    [Parameter]
    public ClientDetailVm Model { get; set; }

    protected void OnClickAccept()
    {
        _dialogService.Close();
    }
}