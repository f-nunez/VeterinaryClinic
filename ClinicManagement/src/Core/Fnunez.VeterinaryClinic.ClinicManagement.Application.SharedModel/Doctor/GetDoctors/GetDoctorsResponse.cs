using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctors;

public class GetDoctorsResponse : BaseResponse
{
    public List<DoctorDto> Doctors { get; set; } = new List<DoctorDto>();
    public int Count { get; set; }

    public GetDoctorsResponse(Guid correlationId) : base(correlationId)
    {
    }
}