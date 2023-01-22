using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.CreateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.UpdateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.AppointmentTypes;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.AppointmentTypes;

public partial class AddEditAppointmentTypeComponent : ComponentBase
{
    [Inject]
    private IAppointmentTypeService _appointmentTypeService { get; set; }

    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    protected bool IsSaving { get; set; }

    [Inject]
    protected IStringLocalizer<AddEditAppointmentTypeComponent> StringLocalizer { get; set; }

    [Parameter]
    public bool IsAppointmentTypeToAdd { get; set; }

    [Parameter]
    public AppointmentTypeVm Model { get; set; } = new();

    protected async void OnSubmit()
    {
        _spinnerService.Show();

        IsSaving = true;

        if (IsAppointmentTypeToAdd)
            await CreateAppointmentTypeAsync();
        else
            await UpdateAppointmentTypeAsync();

        IsSaving = false;

        _spinnerService.Hide();

        _dialogService.Close(Model);
    }

    private async Task CreateAppointmentTypeAsync()
    {
        var request = new CreateAppointmentTypeRequest
        {
            Code = Model.Code,
            Duration = Model.Duration.Value,
            Name = Model.Name
        };

        await _appointmentTypeService.CreateAsync(request);
    }

    private async Task UpdateAppointmentTypeAsync()
    {
        var request = new UpdateAppointmentTypeRequest
        {
            Code = Model.Code,
            Duration = Model.Duration.Value,
            Id = Model.Id,
            Name = Model.Name
        };

        await _appointmentTypeService.UpdateAsync(request);
    }
}