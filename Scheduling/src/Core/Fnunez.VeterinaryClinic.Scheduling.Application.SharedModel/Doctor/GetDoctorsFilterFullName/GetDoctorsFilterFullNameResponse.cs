using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorsFilterFullName;

public class GetDoctorsFilterFullNameResponse : BaseResponse
{
    public List<string> DoctorFullNames { get; set; } = new();

    public GetDoctorsFilterFullNameResponse(Guid correlationId)
        : base(correlationId)
    {

    }
}