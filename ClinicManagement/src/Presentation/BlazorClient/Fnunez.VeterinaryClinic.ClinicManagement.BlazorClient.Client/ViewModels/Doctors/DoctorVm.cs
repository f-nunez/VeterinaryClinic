namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Doctors;

public class DoctorVm
{
    public string FullName { get; set; } = string.Empty;
    public int Id { get; set; }
    public bool IsActive { get; set; }
}