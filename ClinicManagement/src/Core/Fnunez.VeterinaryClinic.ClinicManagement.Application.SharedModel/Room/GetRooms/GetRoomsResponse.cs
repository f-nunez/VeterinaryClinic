using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRooms;

public class GetRoomsResponse : BaseResponse
{
    public List<RoomDto> Rooms { get; set; } = new List<RoomDto>();

    public int Count { get; set; }

    public GetRoomsResponse(Guid correlationId) : base(correlationId)
    {
    }
}