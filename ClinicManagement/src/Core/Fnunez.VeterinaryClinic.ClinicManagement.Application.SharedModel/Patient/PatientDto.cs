namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient;

public class PatientDto
{
    public int PatientId { get; set; }
    public int ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string ImageData { get; set; } = string.Empty;
    public int? PreferredDoctorId { get; set; }
}