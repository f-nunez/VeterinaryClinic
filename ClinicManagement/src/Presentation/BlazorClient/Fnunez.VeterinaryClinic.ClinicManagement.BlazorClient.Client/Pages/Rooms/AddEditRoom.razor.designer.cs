using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.CreateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.UpdateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Rooms;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Rooms;

public partial class AddEditRoomComponent : ComponentBase
{
    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private IRoomService _roomService { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    protected bool IsSaving { get; set; }

    [Inject]
    protected IStringLocalizer<AddEditRoomComponent> StringLocalizer { get; set; }

    [Parameter]
    public bool IsRoomToAdd { get; set; }

    [Parameter]
    public RoomVm Model { get; set; } = new();

    protected async void OnSubmit()
    {
        _spinnerService.Show();

        IsSaving = true;

        if (IsRoomToAdd)
            await CreateRoomAsync();
        else
            await UpdateRoomAsync();

        IsSaving = false;

        _spinnerService.Hide();

        _dialogService.Close(Model);
    }

    private async Task CreateRoomAsync()
    {
        var request = new CreateRoomRequest
        {
            Name = Model.Name
        };

        await _roomService.CreateAsync(request);
    }

    private async Task UpdateRoomAsync()
    {
        var request = new UpdateRoomRequest
        {
            Id = Model.Id,
            Name = Model.Name
        };

        await _roomService.UpdateAsync(request);
    }
}