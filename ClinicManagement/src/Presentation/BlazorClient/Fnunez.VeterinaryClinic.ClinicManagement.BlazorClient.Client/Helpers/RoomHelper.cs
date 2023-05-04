using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Rooms;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;

public static class RoomHelper
{
    public static RoomVm MapRoomViewModel(RoomDto roomDto)
    {
        return new RoomVm
        {
            Id = roomDto.Id,
            IsActive = roomDto.IsActive,
            Name = roomDto.Name
        };
    }
}