using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomsFilterName;

public class GetRoomsFilterNameRequest : BaseRequest
{
    public string NameFilterValue { get; set; } = null!;
}