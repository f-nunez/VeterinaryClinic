using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Models.Patients;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Patients;

public class AddEditPatientVm
{
    public string Breed { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsNewPhoto { get; set; }
    public int PatientId { get; set; }
    public byte[] PhotoData { get; set; } = null!;
    public string PhotoName { get; set; } = string.Empty;
    public int? PreferredDoctorId { get; set; }
    public AnimalSex? Sex { get; set; }
    public string Species { get; set; } = string.Empty;
}