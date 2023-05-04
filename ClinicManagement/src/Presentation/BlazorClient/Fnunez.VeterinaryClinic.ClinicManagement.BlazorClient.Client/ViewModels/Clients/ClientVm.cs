namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Clients;

public class ClientVm
{
    public int ClientId { get; set; }
    public string EmailAddress { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public int? PreferredDoctorId { get; set; }
    public int PreferredLanguage { get; set; }
    public string PreferredName { get; set; } = string.Empty;
    public string Salutation { get; set; } = string.Empty;
}