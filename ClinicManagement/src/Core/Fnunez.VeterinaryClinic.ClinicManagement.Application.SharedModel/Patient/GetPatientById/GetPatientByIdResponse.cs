using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientById;

public class GetPatientByIdResponse : BaseResponse
{
    public PatientDto Patient { get; set; } = new PatientDto();

    public GetPatientByIdResponse(Guid correlationId) : base(correlationId)
    {
    }
}