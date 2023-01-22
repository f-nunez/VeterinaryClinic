using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Models.Patients;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Patients;

public class PatientDetailVm
{
    public string Breed { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PhotoBase64Encoded { get; set; } = string.Empty;
    public string PhotoName { get; set; } = string.Empty;
    public string PreferredDoctorFullName { get; set; } = string.Empty;
    public AnimalSex Sex { get; set; }
    public string Species { get; set; } = string.Empty;
}