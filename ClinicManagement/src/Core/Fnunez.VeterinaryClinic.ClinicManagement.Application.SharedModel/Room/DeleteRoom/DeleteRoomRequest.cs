using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.DeleteRoom;

public class DeleteRoomRequest : BaseRequest
{
    public int Id { get; set; }
}