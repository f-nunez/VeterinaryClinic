namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;

public class PatientDeletedIntegrationEvent : BaseIntegrationEvent
{
    public int PatientClientId { get; set; }
    public int PatientId { get; set; }
}