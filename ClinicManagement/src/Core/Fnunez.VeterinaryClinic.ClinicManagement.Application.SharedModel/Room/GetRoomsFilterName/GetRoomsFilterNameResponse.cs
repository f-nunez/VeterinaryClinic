using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomsFilterName;

public class GetRoomsFilterNameResponse : BaseResponse
{
    public List<string> RoomNames { get; set; } = new();

    public GetRoomsFilterNameResponse(Guid correlationId) : base(correlationId)
    {
    }
}