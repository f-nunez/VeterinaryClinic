using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Appointments;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Appointments;

public partial class AppointmentDetailComponent : ComponentBase
{
    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    protected IStringLocalizer<AppointmentDetailComponent> StringLocalizer { get; set; }

    protected string DoctorBase64EncodedImageData = string.Empty;

    protected string PatientBase64EncodedImageData = string.Empty;

    [Parameter]
    public AppointmentDetailVm Appointment { get; set; }

    protected override void OnInitialized()
    {
        DoctorBase64EncodedImageData = AppointmentHelper.GetDemoDoctorPhoto();
        PatientBase64EncodedImageData = AppointmentHelper.GetDemoPatientPhoto();
    }

    protected async void Delete()
    {
        string message = string.Format(
            StringLocalizer["AppointmentDetail_DeleteAppointment_Alert_Message"],
            Appointment.Title);

        bool? proceedToDelete = await _dialogService.Confirm(
            message,
            StringLocalizer["AppointmentDetail_DeleteAppointment_Alert_Title"],
            new ConfirmOptions
            {
                OkButtonText = StringLocalizer["AppointmentDetail_DeleteAppointment_Alert_Button_Ok"],
                CancelButtonText = StringLocalizer["AppointmentDetail_DeleteAppointment_Alert_Button_Cancel"]
            }
        );

        if (proceedToDelete.HasValue && proceedToDelete.Value)
            _dialogService.Close(AppointmentDetailResponse.Delete);
    }

    protected void Edit()
    {
        _dialogService.Close(AppointmentDetailResponse.Edit);
    }
}