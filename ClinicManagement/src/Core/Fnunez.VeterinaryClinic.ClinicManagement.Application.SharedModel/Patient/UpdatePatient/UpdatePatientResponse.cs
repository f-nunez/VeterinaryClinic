using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.UpdatePatient;

public class UpdatePatientResponse : BaseResponse
{
    public PatientDto Patient { get; set; } = new PatientDto();

    public UpdatePatientResponse(Guid correlationId) : base(correlationId)
    {
    }
}