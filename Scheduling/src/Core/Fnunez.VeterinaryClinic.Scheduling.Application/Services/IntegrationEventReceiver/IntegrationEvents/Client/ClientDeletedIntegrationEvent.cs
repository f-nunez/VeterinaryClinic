namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;

public class ClientDeletedIntegrationEvent : BaseIntegrationEvent
{
    public int ClientId { get; set; }
}