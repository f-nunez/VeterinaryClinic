using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomById;

public class GetRoomByIdRequest : BaseRequest
{
    public int Id { get; set; }
}