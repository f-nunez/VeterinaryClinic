using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.CreateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.DeleteRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.UpdateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages;

public partial class RoomsPage
{
    private List<RoomDto> Rooms = new();
    private RoomDto ToSave = new();
    [Inject]
    IJSRuntime? JSRuntime { get; set; }
    [Inject]
    RoomService? RoomService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await ReloadData();
    }

    private void CreateClick()
    {
        if (Rooms.Count == 0 || Rooms[Rooms.Count - 1].Id != 0)
        {
            ToSave = new RoomDto();
            Rooms.Add(ToSave);
        }
    }

    private void EditClick(int id)
    {
        var room = Rooms.Find(x => x.Id == id);

        if (room is null)
            throw new ArgumentNullException(nameof(room));

        ToSave = room;
    }

    private async Task DeleteClick(int id)
    {
        if (JSRuntime is null)
            return;

        if (RoomService is null)
            return;

        bool confirmed = await JSRuntime
            .InvokeAsync<bool>("confirm", "Are you sure?");

        if (confirmed)
        {
            var toDelete = new DeleteRoomRequest()
            {
                Id = id,
            };
            await RoomService.DeleteAsync(id);
            await ReloadData();
        }
    }

    private async Task SaveClick()
    {
        if (RoomService is null)
            return;

        if (ToSave.Id == 0)
        {
            var toCreate = new CreateRoomRequest()
            {
                Name = ToSave.Name,
            };
            await RoomService.CreateAsync(toCreate);
        }
        else
        {
            var toUpdate = new UpdateRoomRequest()
            {
                Id = ToSave.Id,
                Name = ToSave.Name,
            };
            await RoomService.EditAsync(toUpdate);
        }

        CancelClick();
        await ReloadData();
    }

    private void CancelClick()
    {
        if (ToSave.Id == 0)
            Rooms.RemoveAt(Rooms.Count - 1);

        ToSave = new RoomDto();
    }

    private bool IsAddOrEdit(int id)
    {
        return ToSave.Id == id;
    }

    private async Task ReloadData()
    {
        if (RoomService is null)
            return;
        await Task.Delay(1);
        throw new NotImplementedException();
        // Rooms = await RoomService.ListAsync();
    }
}