using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorsFilterId;

public class GetDoctorsFilterIdResponse : BaseResponse
{
    public List<string> DoctorIds { get; set; } = new();

    public GetDoctorsFilterIdResponse(Guid correlationId) : base(correlationId)
    {
    }
}