using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Appointments;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Appointments;

public partial class AppointmentDetailNotificationComponent : ComponentBase
{
    [Inject]
    private IAppointmentService _appointmentService { get; set; }

    [Inject]
    private ILogger<AppointmentDetailNotificationComponent> _logger { get; set; }

    [Inject]
    private NavigationManager _navigationManager { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    [Inject]
    private IUserSettingsService _userSettingsService { get; set; }

    protected string DoctorPhotoBase64Encoded { get; set; }

    protected bool IsNotActive { get; set; }

    protected bool IsNotFound { get; set; }

    protected AppointmentDetailVm Model { get; set; } = new();

    [Inject]
    protected IStringLocalizer<AppointmentDetailNotificationComponent> StringLocalizer { get; set; }

    [Parameter]
    public Guid AppointmentId { get; set; }

    protected override void OnInitialized()
    {
        DoctorPhotoBase64Encoded = DoctorHelper.GetDoctorThumbnail();

        Model.PatientPhotoBase64Encoded = PatientHelper.GetPatientThumbnail();
    }

    protected override async Task OnParametersSetAsync()
    {
        _spinnerService.Show();

        var request = new GetAppointmentDetailRequest
        {
            AppointmentId = AppointmentId
        };

        try
        {
            var response = await _appointmentService
                .GetAppointmentDetailAsync(request);

            Model = AppointmentHelper.MapAppointmentDetailViewModel(
                response.Appointment,
                await _userSettingsService.GetTimeZoneNameAsync(),
                await _userSettingsService.GetUtcOffsetInMinutesAsync()
            );

            IsNotActive = !Model.IsActive;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            IsNotFound = true;
        }

        _spinnerService.Hide();
    }

    protected void OnClickBack()
    {
        _navigationManager.NavigateTo("appointments");
    }
}