using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.UpdateRoom;

public class UpdateRoomRequest : BaseRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}