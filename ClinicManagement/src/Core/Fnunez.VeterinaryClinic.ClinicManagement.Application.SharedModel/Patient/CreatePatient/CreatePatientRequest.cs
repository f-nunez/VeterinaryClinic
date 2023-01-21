using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.CreatePatient;

public class CreatePatientRequest : BaseRequest
{
    public string Breed { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public byte[] PhotoData { get; set; } = null!;
    public string PhotoName { get; set; } = string.Empty;
    public int? PreferredDoctorId { get; set; }
    public int Sex { get; set; }
    public string Species { get; set; } = string.Empty;
}