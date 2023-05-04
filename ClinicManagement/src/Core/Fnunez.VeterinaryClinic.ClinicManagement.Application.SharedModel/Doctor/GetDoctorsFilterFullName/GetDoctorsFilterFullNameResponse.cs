using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorsFilterFullName;

public class GetDoctorsFilterFullNameResponse : BaseResponse
{
    public List<string> DoctorFullNames { get; set; } = new();

    public GetDoctorsFilterFullNameResponse(Guid correlationId)
        : base(correlationId)
    {

    }
}