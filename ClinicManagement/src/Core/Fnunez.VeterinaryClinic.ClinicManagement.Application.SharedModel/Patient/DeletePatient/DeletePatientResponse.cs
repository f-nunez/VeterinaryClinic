using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.DeletePatient;

public class DeletePatientResponse : BaseResponse
{
    public DeletePatientResponse(Guid correlationId) : base(correlationId)
    {
    }
}