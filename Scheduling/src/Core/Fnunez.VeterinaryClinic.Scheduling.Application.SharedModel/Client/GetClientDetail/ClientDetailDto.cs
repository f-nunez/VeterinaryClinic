namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientDetail;

public class ClientDetailDto
{
    public int ClientId { get; set; }
    public string EmailAddress { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PreferredDoctorFullName { get; set; } = string.Empty;
    public string PreferredName { get; set; } = string.Empty;
    public string Salutation { get; set; } = string.Empty;
}