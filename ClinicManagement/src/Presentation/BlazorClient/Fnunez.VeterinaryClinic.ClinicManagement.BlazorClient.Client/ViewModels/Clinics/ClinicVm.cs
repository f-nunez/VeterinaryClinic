namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Clinics;

public class ClinicVm
{
    public string Address { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; } = string.Empty;
}