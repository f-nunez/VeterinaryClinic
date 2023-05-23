namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;

public class RoomUpdatedIntegrationEvent : BaseIntegrationEvent
{
    public int RoomId { get; set; }
    public string RoomName { get; set; } = string.Empty;
}