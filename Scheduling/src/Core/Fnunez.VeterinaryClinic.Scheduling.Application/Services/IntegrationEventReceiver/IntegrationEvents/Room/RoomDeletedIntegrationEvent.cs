namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;

public class RoomDeletedIntegrationEvent : BaseIntegrationEvent
{
    public int RoomId { get; set; }
}