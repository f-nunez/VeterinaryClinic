using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomById;

public class GetRoomByIdRequest : BaseRequest
{
    public int RoomId { get; set; }
}