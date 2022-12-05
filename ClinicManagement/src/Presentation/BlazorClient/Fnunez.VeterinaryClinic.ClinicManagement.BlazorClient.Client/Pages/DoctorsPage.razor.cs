using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.CreateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.UpdateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages;

public partial class DoctorsPage
{
    private List<DoctorDto> Doctors = new();
    private DoctorDto ToSave = new();
    [Inject]
    IJSRuntime? JSRuntime { get; set; }
    [Inject]
    DoctorService? DoctorService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await ReloadData();
    }

    private void CreateClick()
    {
        if (Doctors.Count == 0 || Doctors[Doctors.Count - 1].Id != 0)
        {
            ToSave = new DoctorDto();
            Doctors.Add(ToSave);
        }
    }

    private void EditClick(int id)
    {
        var doctor = Doctors.Find(x => x.Id == id);

        if (doctor is null)
            throw new ArgumentNullException(nameof(doctor));

        ToSave = doctor;
    }

    private async Task DeleteClick(int id)
    {
        if (JSRuntime is null)
            return;

        if (DoctorService is null)
            return;

        bool confirmed = await JSRuntime
            .InvokeAsync<bool>("confirm", "Are you sure?");

        if (confirmed)
        {
            await DoctorService.DeleteAsync(id);
            await ReloadData();
        }
    }

    private async Task SaveClick()
    {
        if (DoctorService is null)
            return;

        if (ToSave.Id == 0)
        {
            var toCreate = new CreateDoctorRequest()
            {
                FullName = ToSave.FullName,
            };
            await DoctorService.CreateAsync(toCreate);
        }
        else
        {
            var toUpdate = new UpdateDoctorRequest()
            {
                Id = ToSave.Id,
                FullName = ToSave.FullName,
            };
            await DoctorService.EditAsync(toUpdate);
        }

        CancelClick();
        await ReloadData();
    }

    private void CancelClick()
    {
        if (ToSave.Id == 0)
            Doctors.RemoveAt(Doctors.Count - 1);

        ToSave = new DoctorDto();
    }

    private bool IsAddOrEdit(int id)
    {
        return ToSave.Id == id;
    }

    private async Task ReloadData()
    {
        if (DoctorService is null)
            return;

        Doctors = await DoctorService.ListAsync();
    }
}