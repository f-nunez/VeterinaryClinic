using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypeById;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.AppointmentTypes;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.AppointmentTypes;

public partial class AppointmentTypeDetailNotificationComponent : ComponentBase
{
    [Inject]
    private IAppointmentTypeService _appointmentTypeService { get; set; }

    [Inject]
    private ILogger<AppointmentTypeDetailNotificationComponent> _logger { get; set; }

    [Inject]
    private NavigationManager _navigationManager { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    protected bool IsNotActive { get; set; }

    protected bool IsNotFound { get; set; }

    protected AppointmentTypeVm Model { get; set; } = new();

    [Inject]
    protected IStringLocalizer<AppointmentTypeDetailNotificationComponent> StringLocalizer { get; set; }

    [Parameter]
    public int AppointmentTypeId { get; set; }

    protected async override Task OnParametersSetAsync()
    {
        _spinnerService.Show();

        var request = new GetAppointmentTypeByIdRequest
        {
            Id = AppointmentTypeId
        };

        try
        {
            var appointmentType = await _appointmentTypeService
                .GetByIdAsync(request);

            Model = AppointmentTypeHelper
                .MapAppointmentTypeViewModel(appointmentType);

            IsNotActive = !Model.IsActive;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            IsNotFound = true;
        }

        _spinnerService.Hide();
    }

    protected void OnClickBack()
    {
        _navigationManager.NavigateTo("appointment-types");
    }
}