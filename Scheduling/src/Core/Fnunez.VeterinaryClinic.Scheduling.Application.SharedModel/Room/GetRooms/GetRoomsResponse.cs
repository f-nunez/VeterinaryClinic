using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRooms;

public class GetRoomsResponse : BaseResponse
{
    public List<RoomDto> Rooms { get; set; } = new();
    public int Count { get; set; }

    public GetRoomsResponse(Guid correlationId) : base(correlationId)
    {
    }
}
