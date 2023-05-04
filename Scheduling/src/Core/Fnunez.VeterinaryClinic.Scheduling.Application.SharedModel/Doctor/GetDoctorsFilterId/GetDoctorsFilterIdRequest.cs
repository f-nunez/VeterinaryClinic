using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorsFilterId;

public class GetDoctorsFilterIdRequest : BaseRequest
{
    public string IdFilterValue { get; set; } = null!;
}