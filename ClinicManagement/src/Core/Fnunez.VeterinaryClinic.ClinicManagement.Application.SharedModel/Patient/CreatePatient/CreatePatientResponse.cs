using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.CreatePatient;

public class CreatePatientResponse : BaseResponse
{
    public CreatePatientResponse(Guid correlationId) : base(correlationId)
    {
    }
}