using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicById;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Clinics;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Clinics;

public partial class ClinicDetailNotificationComponent : ComponentBase
{
    [Inject]
    private IClinicService _clinicService { get; set; }

    [Inject]
    private ILogger<ClinicDetailNotificationComponent> _logger { get; set; }

    [Inject]
    private NavigationManager _navigationManager { get; set; }

    protected bool IsNotActive { get; set; }

    protected bool IsNotFound { get; set; }

    protected ClinicVm Model { get; set; } = new();

    [Inject]
    protected IStringLocalizer<ClinicDetailNotificationComponent> StringLocalizer { get; set; }

    [Parameter]
    public int ClinicId { get; set; }

    protected async override Task OnParametersSetAsync()
    {
        var request = new GetClinicByIdRequest
        {
            Id = ClinicId
        };

        try
        {
            var clinic = await _clinicService
                .GetByIdAsync(request);

            Model = ClinicHelper
                .MapClinicViewModel(clinic);

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
        _navigationManager.NavigateTo("clinics");
    }
}