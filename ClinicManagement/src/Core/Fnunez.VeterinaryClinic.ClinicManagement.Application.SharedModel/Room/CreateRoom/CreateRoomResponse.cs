using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.CreateRoom;

public class CreateRoomResponse : BaseResponse
{
    public RoomDto Room { get; set; } = new RoomDto();

    public CreateRoomResponse(Guid correlationId) : base(correlationId)
    {
    }
}