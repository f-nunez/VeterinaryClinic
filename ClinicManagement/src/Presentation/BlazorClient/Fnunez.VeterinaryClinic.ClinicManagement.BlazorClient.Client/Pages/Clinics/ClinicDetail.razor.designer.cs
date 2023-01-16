using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Clinics;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Clinics;

public partial class ClinicDetailComponent : ComponentBase
{
    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    protected IStringLocalizer<ClinicDetailComponent> StringLocalizer { get; set; }

    [Parameter]
    public ClinicVm Model { get; set; }

    protected void OnClickAccept()
    {
        _dialogService.Close();
    }
}