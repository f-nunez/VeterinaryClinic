using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomById;

public class GetRoomByIdResponse : BaseResponse
{
    public RoomDto Room { get; set; } = new RoomDto();

    public GetRoomByIdResponse(Guid correlationId) : base(correlationId)
    {
    }
}