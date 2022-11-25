namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;

public class ClientDto
{
    public int ClientId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string Salutation { get; set; } = string.Empty;
    public string PreferredName { get; set; } = string.Empty;
    public int? PreferredDoctorId { get; set; }
    public IList<int> Patients { get; set; } = new List<int>();
}