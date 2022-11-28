using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.CreatePatient;

public class CreatePatientRequest : BaseRequest
{
    public int ClientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
}