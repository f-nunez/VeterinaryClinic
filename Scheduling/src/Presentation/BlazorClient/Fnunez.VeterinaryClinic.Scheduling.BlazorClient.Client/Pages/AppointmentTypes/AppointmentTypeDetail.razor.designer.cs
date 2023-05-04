using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.AppointmentTypes;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.AppointmentTypes;

public partial class AppointmentTypeDetailComponent : ComponentBase
{
    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    protected IStringLocalizer<AppointmentTypeDetailComponent> StringLocalizer { get; set; }

    [Parameter]
    public AppointmentTypeVm Model { get; set; }

    protected void OnClickAccept()
    {
        _dialogService.Close();
    }
}