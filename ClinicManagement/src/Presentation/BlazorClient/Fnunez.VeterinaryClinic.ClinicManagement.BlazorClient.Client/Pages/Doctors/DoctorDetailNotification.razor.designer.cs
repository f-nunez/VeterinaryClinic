using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorById;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Doctors;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Doctors;

public partial class DoctorDetailNotificationComponent : ComponentBase
{
    [Inject]
    private IDoctorService _doctorService { get; set; }

    [Inject]
    private ILogger<DoctorDetailNotificationComponent> _logger { get; set; }

    [Inject]
    private NavigationManager _navigationManager { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    protected bool IsNotActive { get; set; }

    protected bool IsNotFound { get; set; }

    protected DoctorVm Model { get; set; } = new();

    [Inject]
    protected IStringLocalizer<DoctorDetailNotificationComponent> StringLocalizer { get; set; }

    [Parameter]
    public int DoctorId { get; set; }

    protected async override Task OnParametersSetAsync()
    {
        _spinnerService.Show();

        var request = new GetDoctorByIdRequest
        {
            Id = DoctorId
        };

        try
        {
            var doctor = await _doctorService
                .GetByIdAsync(request);

            Model = DoctorHelper
                .MapDoctorViewModel(doctor);

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
        _navigationManager.NavigateTo("doctors");
    }
}