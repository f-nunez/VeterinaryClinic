namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Clients;

public class ClientDetailVm
{
    public int ClientId { get; set; }
    public string EmailAddress { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PreferredDoctorFullName { get; set; } = string.Empty;
    public int PreferredLanguage { get; set; }
    public string PreferredName { get; set; } = string.Empty;
    public string Salutation { get; set; } = string.Empty;
}