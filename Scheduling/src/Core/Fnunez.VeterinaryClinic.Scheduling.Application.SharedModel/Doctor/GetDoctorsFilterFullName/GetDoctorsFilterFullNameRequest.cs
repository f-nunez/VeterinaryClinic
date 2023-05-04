using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorsFilterFullName;

public class GetDoctorsFilterFullNameRequest : BaseRequest
{
    public string FullNameFilterValue { get; set; } = null!;
}