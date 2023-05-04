using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomsFilterId;

public class GetRoomsFilterIdRequest : BaseRequest
{
    public string IdFilterValue { get; set; } = null!;
}