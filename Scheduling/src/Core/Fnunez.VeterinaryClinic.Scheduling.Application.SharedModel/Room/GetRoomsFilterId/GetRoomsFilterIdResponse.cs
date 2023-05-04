using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomsFilterId;

public class GetRoomsFilterIdResponse : BaseResponse
{
    public List<string> RoomIds { get; set; } = new();
    
    public GetRoomsFilterIdResponse(Guid correlationId) : base(correlationId)
    {
    }
}