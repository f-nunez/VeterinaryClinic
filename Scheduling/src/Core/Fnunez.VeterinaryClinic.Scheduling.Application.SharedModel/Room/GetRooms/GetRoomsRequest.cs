using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRooms;

public class GetRoomsRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
    public string IdFilterValue { get; set; } = null!;
    public string NameFilterValue { get; set; } = null!;
    public string SearchFilterValue { get; set; } = null!;
}