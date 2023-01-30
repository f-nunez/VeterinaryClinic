using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Rooms;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Rooms;

public partial class RoomDetailComponent : ComponentBase
{
    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    protected IStringLocalizer<RoomDetailComponent> StringLocalizer { get; set; }

    [Parameter]
    public RoomVm Model { get; set; }

    protected void OnClickAccept()
    {
        _dialogService.Close();
    }
}