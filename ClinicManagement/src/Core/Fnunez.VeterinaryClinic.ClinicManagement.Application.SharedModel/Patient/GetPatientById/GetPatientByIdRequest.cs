using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientById;

public class GetPatientByIdRequest : BaseRequest
{
    public int ClientId { get; set; }
    public int PatientId { get; set; }
}