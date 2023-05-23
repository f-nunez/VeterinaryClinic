namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;

public class RoomUpdatedIntegrationEvent : BaseIntegrationEvent
{
    public int RoomId { get; set; }
    public string RoomName { get; set; } = string.Empty;
}