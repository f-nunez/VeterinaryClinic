using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.DeleteRoom;

public class DeleteRoomResponse : BaseResponse
{
    public DeleteRoomResponse(Guid correlationId) : base(correlationId)
    {
    }
}