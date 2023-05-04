using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.CreateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.UpdateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Clinics;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Clinics;

public partial class AddEditClinicComponent : ComponentBase
{
    [Inject]
    private IClinicService _clinicService { get; set; }

    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    protected bool IsSaving { get; set; }

    [Inject]
    protected IStringLocalizer<AddEditClinicComponent> StringLocalizer { get; set; }

    [Parameter]
    public bool IsClinicToAdd { get; set; }

    [Parameter]
    public ClinicVm Model { get; set; } = new();

    protected async void OnSubmit()
    {
        _spinnerService.Show();

        IsSaving = true;

        if (IsClinicToAdd)
            await CreateClinicAsync();
        else
            await UpdateClinicAsync();

        IsSaving = false;

        _spinnerService.Hide();

        _dialogService.Close(Model);
    }

    private async Task CreateClinicAsync()
    {
        var request = new CreateClinicRequest
        {
            Address = Model.Address,
            EmailAddress = Model.EmailAddress,
            Name = Model.Name
        };

        await _clinicService.CreateAsync(request);
    }

    private async Task UpdateClinicAsync()
    {
        var request = new UpdateClinicRequest
        {
            Address = Model.Address,
            EmailAddress = Model.EmailAddress,
            Id = Model.Id,
            Name = Model.Name
        };

        await _clinicService.UpdateAsync(request);
    }
}