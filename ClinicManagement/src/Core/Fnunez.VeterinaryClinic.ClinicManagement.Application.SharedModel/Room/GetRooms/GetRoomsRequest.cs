using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRooms;

public class GetRoomsRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
    public string IdFilterValue { get; set; } = null!;
    public string NameFilterValue { get; set; } = null!;
}