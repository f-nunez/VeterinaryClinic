using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.CreateDoctor;

public class CreateDoctorResponse : BaseResponse
{
    public DoctorDto Doctor { get; set; } = new DoctorDto();

    public CreateDoctorResponse(Guid correlationId) : base(correlationId)
    {
    }
}