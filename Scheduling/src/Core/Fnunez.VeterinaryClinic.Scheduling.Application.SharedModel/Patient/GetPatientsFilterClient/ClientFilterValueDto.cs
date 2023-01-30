namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatientsFilterClient;

public class ClientFilterValueDto
{
    public string EmailAddress { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public int Id { get; set; }
}