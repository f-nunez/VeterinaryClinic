using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomsFilterName;

public class GetRoomsFilterNameRequest : BaseRequest
{
    public string NameFilterValue { get; set; } = null!;
}