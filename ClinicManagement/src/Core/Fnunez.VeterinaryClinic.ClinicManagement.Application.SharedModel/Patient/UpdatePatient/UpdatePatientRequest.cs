using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.UpdatePatient;

public class UpdatePatientRequest : BaseRequest
{
    public int ClientId { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int? PreferredDoctorId { get; set; }
}