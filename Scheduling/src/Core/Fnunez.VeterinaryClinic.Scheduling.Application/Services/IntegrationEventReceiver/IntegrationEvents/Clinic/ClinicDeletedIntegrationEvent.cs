namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;

public class ClinicDeletedIntegrationEvent : BaseIntegrationEvent
{
    public int ClinicId { get; set; }
}