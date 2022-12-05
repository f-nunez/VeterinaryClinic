using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.CreateRoom;

public class CreateRoomRequest : BaseRequest
{
    public string? Name { get; set; }

    public override string ToString()
    {
        return $"Room: Name = {Name}";
    }
}