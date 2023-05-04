using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorById;

public class GetDoctorByIdResponse : BaseResponse
{
    public DoctorDto Doctor { get; set; } = new DoctorDto();

    public GetDoctorByIdResponse(Guid correlationId) : base(correlationId)
    {
    }
}