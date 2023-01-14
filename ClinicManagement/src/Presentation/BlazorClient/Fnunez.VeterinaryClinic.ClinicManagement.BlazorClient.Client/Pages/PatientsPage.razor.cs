using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.UpdatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages;

public partial class PatientsPage
{
    private List<ClientDto> Clients = new();
    private List<PatientDto> Patients = new();
    private PatientDto ToSave = new();
    [Inject]
    IJSRuntime? JSRuntime { get; set; }
    [Inject]
    ClientService? ClientService { get; set; }
    [Inject]
    PatientService? PatientService { get; set; }
    private string? _selectedClientId { get; set; }
    public string? SelectedClientId
    {
        get
        {
            return _selectedClientId;
        }
        set
        {
            _selectedClientId = value;
            ReloadPatientData().GetAwaiter();
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await ReloadClientData();
    }

    private async Task OnChangeSelectedClientId(string selectedClientId)
    {
        if (JSRuntime is null)
            return;

        SelectedClientId = selectedClientId;
        await ReloadPatientData();
    }

    private void CreateClick()
    {
        if (Patients.Count == 0 || Patients[Patients.Count - 1].PatientId != 0)
        {
            ToSave = new PatientDto();
            Patients.Add(ToSave);
        }
    }

    private void EditClick(int id)
    {
        var patient = Patients.Find(x => x.PatientId == id);

        if (patient is null)
            throw new ArgumentNullException(nameof(patient));

        ToSave = patient;
    }

    private async Task DeleteClick(int clientId, int patientId)
    {
        if (PatientService is null)
            return;

        if (JSRuntime is null)
            return;

        bool confirmed = await JSRuntime
            .InvokeAsync<bool>("confirm", "Are you sure?");

        if (confirmed)
        {
            await PatientService.DeleteAsync(clientId, patientId);
            await ReloadPatientData();
        }
    }

    private async Task SaveClick()
    {
        if (PatientService is null)
            return;
        
        if (int.TryParse(SelectedClientId, out int clientId))
            ToSave.ClientId = clientId;

        if (ToSave.PatientId == 0)
        {
            var toCreate = new CreatePatientRequest()
            {
                ClientId = ToSave.ClientId,
                PatientName = ToSave.PatientName,
                PreferredDoctorId = ToSave.PreferredDoctorId
            };
            await PatientService.CreateAsync(toCreate);
        }
        else
        {
            var toUpdate = new UpdatePatientRequest()
            {
                ClientId = ToSave.ClientId,
                PatientId = ToSave.PatientId,
                PatientName = ToSave.PatientName,
                PreferredDoctorId = ToSave.PreferredDoctorId
            };

            await PatientService.EditAsync(toUpdate);
        }

        CancelClick();
        await ReloadPatientData();
    }

    private void CancelClick()
    {
        if (ToSave.PatientId == 0)
            Patients.RemoveAt(Patients.Count - 1);

        ToSave = new PatientDto();
    }

    private bool IsAddOrEdit(int id)
    {
        return ToSave.PatientId == id;
    }

    private async Task ReloadClientData()
    {
        if (ClientService is null)
            return;
        await Task.Delay(1);
        throw new NotImplementedException();
        // Clients = await ClientService.ListAsync();
        // Patients = new List<PatientDto>();

        // StateHasChanged();
    }

    private async Task ReloadPatientData()
    {
        if (PatientService is null)
            return;
        await Task.Delay(1);
        throw new NotImplementedException();
        // if (int.TryParse(SelectedClientId, out int clientId))

        // StateHasChanged();
    }
}