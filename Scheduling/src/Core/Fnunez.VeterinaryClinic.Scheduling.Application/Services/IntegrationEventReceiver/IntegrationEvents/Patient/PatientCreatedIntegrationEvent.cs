namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;

public class PatientCreatedIntegrationEvent : BaseIntegrationEvent
{
    public string PatientBreed { get; set; } = string.Empty;
    public int PatientClientId { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string PatientPhotoName { get; set; } = string.Empty;
    public string PatientPhotoStoredName { get; set; } = string.Empty;
    public int? PatientPreferredDoctorId { get; set; }
    public int PatientSex { get; set; }
    public string PatientSpecies { get; set; } = string.Empty;
}