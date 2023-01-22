using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRooms;

public class GetRoomsResponse : BaseResponse
{
    public DataGridResponse<RoomDto>? DataGridResponse { get; set; }

    public GetRoomsResponse(Guid correlationId) : base(correlationId)
    {
    }
}
