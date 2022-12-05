using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.DeletePatient;

public class DeletePatientRequest : BaseRequest
{
    public int ClientId { get; set; }
    public int PatientId { get; set; }
}