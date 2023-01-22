using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Doctors;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Doctors;

public partial class DoctorDetailComponent : ComponentBase
{
    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    protected IStringLocalizer<DoctorDetailComponent> StringLocalizer { get; set; }

    [Parameter]
    public DoctorVm Model { get; set; }

    protected void OnClickAccept()
    {
        _dialogService.Close();
    }
}