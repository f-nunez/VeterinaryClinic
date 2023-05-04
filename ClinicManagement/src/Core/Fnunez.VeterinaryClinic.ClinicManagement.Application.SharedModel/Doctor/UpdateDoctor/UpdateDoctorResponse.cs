using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.UpdateDoctor;

public class UpdateDoctorResponse : BaseResponse
{
    public DoctorDto Doctor { get; set; } = new DoctorDto();

    public UpdateDoctorResponse(Guid correlationId) : base(correlationId)
    {
    }
}