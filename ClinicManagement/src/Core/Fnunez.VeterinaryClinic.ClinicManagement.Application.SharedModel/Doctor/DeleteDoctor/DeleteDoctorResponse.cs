using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.DeleteDoctor;

public class DeleteDoctorResponse : BaseResponse
{
    public DeleteDoctorResponse(Guid correlationId) : base(correlationId)
    {
    }
}