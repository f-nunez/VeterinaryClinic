using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomById;

public class GetRoomByIdResponse : BaseResponse
{
    public RoomDto Room { get; set; } = new RoomDto();

    public GetRoomByIdResponse(Guid correlationId) : base(correlationId)
    {
    }
}