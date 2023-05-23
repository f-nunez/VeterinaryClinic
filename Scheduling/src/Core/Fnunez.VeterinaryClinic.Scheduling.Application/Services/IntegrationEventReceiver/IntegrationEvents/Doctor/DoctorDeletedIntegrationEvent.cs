namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;

public class DoctorDeletedIntegrationEvent : BaseIntegrationEvent
{
    public int DoctorId { get; set; }
}