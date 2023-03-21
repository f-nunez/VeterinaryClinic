namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatients;

public class PatientsDto
{
    public int ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PatientId { get; set; }
    public byte[]? PhotoData { get; set; } = null!;
    public string PhotoName { get; set; } = string.Empty;
    public int Sex { get; set; }
    public string Species { get; set; } = string.Empty;
}