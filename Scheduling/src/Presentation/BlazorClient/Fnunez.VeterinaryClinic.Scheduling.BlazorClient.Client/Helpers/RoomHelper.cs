using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Rooms;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;

public static class RoomHelper
{
    public static RoomVm MapRoomViewModel(RoomDto roomDto)
    {
        return new RoomVm
        {
            Id = roomDto.Id,
            Name = roomDto.Name
        };
    }
}