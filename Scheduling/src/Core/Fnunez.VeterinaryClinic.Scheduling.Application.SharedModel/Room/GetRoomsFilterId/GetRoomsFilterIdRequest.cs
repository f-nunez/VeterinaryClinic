using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomsFilterId;

public class GetRoomsFilterIdRequest : BaseRequest
{
    public string IdFilterValue { get; set; } = null!;
}