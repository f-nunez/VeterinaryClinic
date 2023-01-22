using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Patients;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Patients;

public partial class PatientDetailComponent : ComponentBase
{
    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    protected IStringLocalizer<PatientDetailComponent> StringLocalizer { get; set; }

    [Parameter]
    public PatientDetailVm Model { get; set; }

    protected void OnClickAccept()
    {
        _dialogService.Close();
    }
}