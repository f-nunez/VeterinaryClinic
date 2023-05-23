namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;

public class RoomCreatedIntegrationEvent : BaseIntegrationEvent
{
    public int RoomId { get; set; }
    public string RoomName { get; set; } = string.Empty;
}