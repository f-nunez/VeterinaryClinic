using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientDetail;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Patients;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Patients;

public partial class PatientDetailNotificationComponent : ComponentBase
{
    [Inject]
    private IPatientService _patientService { get; set; }

    [Inject]
    private ILogger<PatientDetailNotificationComponent> _logger { get; set; }

    [Inject]
    private NavigationManager _navigationManager { get; set; }

    protected bool IsNotActive { get; set; }

    protected bool IsNotFound { get; set; }

    protected PatientDetailVm Model { get; set; } = new();

    [Inject]
    protected IStringLocalizer<PatientDetailNotificationComponent> StringLocalizer { get; set; }

    [Parameter]
    public int ClientId { get; set; }

    [Parameter]
    public int PatientId { get; set; }

    protected async override Task OnInitializedAsync()
    {
        var request = new GetPatientDetailRequest
        {
            ClientId = ClientId,
            PatientId = PatientId
        };

        try
        {
            var response = await _patientService
                .GetPatientDetailAsync(request);

            Model = PatientHelper
                .MapPatientDetailViewModel(response.PatientDetail);

            IsNotActive = !Model.IsActive;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            IsNotFound = true;
        }
    }

    protected void OnClickBack()
    {
        _navigationManager.NavigateTo("patients");
    }
}