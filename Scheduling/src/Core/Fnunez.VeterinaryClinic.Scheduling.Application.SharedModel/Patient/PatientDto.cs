namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient;

public class PatientDto
{
    public int ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string ImageData { get; set; } = string.Empty;
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int? PreferredDoctorId { get; set; }
}