namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientDetail;

public class PatientDetailDto
{
    public bool IsActive { get; set; }
    public string Breed { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PatientId { get; set; }
    public byte[]? PhotoData { get; set; }
    public string PhotoName { get; set; } = string.Empty;
    public string PreferredDoctorFullName { get; set; } = string.Empty;
    public int Sex { get; set; }
    public string Species { get; set; } = string.Empty;
}