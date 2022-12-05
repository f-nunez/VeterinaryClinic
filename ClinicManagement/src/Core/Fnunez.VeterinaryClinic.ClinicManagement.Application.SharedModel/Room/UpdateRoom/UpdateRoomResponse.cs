using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.UpdateRoom;

public class UpdateRoomResponse : BaseResponse
{
    public RoomDto Room { get; set; } = new RoomDto();

    public UpdateRoomResponse(Guid correlationId) : base(correlationId)
    {
    }
}