using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.CreateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.UpdateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Doctors;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Doctors;

public partial class AddEditDoctorComponent : ComponentBase
{
    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private IDoctorService _doctorService { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    protected bool IsSaving { get; set; }

    [Inject]
    protected IStringLocalizer<AddEditDoctorComponent> StringLocalizer { get; set; }

    [Parameter]
    public bool IsDoctorToAdd { get; set; }

    [Parameter]
    public DoctorVm Model { get; set; } = new();

    protected async void OnSubmit()
    {
        _spinnerService.Show();

        IsSaving = true;

        if (IsDoctorToAdd)
            await CreateDoctorAsync();
        else
            await UpdateDoctorAsync();

        IsSaving = false;

        _spinnerService.Hide();

        _dialogService.Close(Model);
    }

    private async Task CreateDoctorAsync()
    {
        var request = new CreateDoctorRequest
        {
            FullName = Model.FullName
        };

        await _doctorService.CreateAsync(request);
    }

    private async Task UpdateDoctorAsync()
    {
        var request = new UpdateDoctorRequest
        {
            FullName = Model.FullName,
            Id = Model.Id
        };

        await _doctorService.UpdateAsync(request);
    }
}