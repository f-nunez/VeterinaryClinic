using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.CreateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.DeleteAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.UpdateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages;

public partial class AppointmentTypesPage
{
    private List<AppointmentTypeDto> AppointmentTypes = new();
    private AppointmentTypeDto ToSave = new();
    [Inject]
    IJSRuntime? JSRuntime { get; set; }
    [Inject]
    AppointmentTypeService? AppointmentTypeService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await ReloadData();
    }

    private void CreateClick()
    {
        if (AppointmentTypes.Count == 0 || AppointmentTypes[AppointmentTypes.Count - 1].Id != 0)
        {
            ToSave = new AppointmentTypeDto();
            AppointmentTypes.Add(ToSave);
        }
    }

    private void EditClick(int id)
    {
        var appointmentType = AppointmentTypes.Find(x => x.Id == id);

        if (appointmentType is null)
            throw new ArgumentNullException(nameof(appointmentType));

        ToSave = appointmentType;
    }

    private async Task DeleteClick(int id)
    {
        if (JSRuntime is null)
            return;

        if (AppointmentTypeService is null)
            return;

        bool confirmed = await JSRuntime
            .InvokeAsync<bool>("confirm", "Are you sure?");

        if (confirmed)
        {
            var toDelete = new DeleteAppointmentTypeRequest()
            {
                Id = id,
            };
            await AppointmentTypeService.DeleteAsync(id);
            await ReloadData();
        }
    }

    private async Task SaveClick()
    {
        if (AppointmentTypeService is null)
            return;

        if (ToSave.Id == 0)
        {
            var toCreate = new CreateAppointmentTypeRequest()
            {
                Code = ToSave.Code,
                Duration = ToSave.Duration,
                Name = ToSave.Name
            };
            await AppointmentTypeService.CreateAsync(toCreate);
        }
        else
        {
            var toUpdate = new UpdateAppointmentTypeRequest()
            {
                Id = ToSave.Id,
                Code = ToSave.Code,
                Duration = ToSave.Duration,
                Name = ToSave.Name
            };
            await AppointmentTypeService.EditAsync(toUpdate);
        }

        CancelClick();
        await ReloadData();
    }

    private void CancelClick()
    {
        if (ToSave.Id == 0)
            AppointmentTypes.RemoveAt(AppointmentTypes.Count - 1);

        ToSave = new AppointmentTypeDto();
    }

    private bool IsAddOrEdit(int id)
    {
        return ToSave.Id == id;
    }

    private async Task ReloadData()
    {
        if (AppointmentTypeService is null)
            return;
        await Task.Delay(1);
        throw new NotImplementedException();
        //AppointmentTypes = await AppointmentTypeService.ListAsync();
    }
}