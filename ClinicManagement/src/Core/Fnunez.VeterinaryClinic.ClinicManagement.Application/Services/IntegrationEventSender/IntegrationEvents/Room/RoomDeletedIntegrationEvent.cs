namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;

public class RoomDeletedIntegrationEvent : BaseIntegrationEvent
{
    public int RoomId { get; set; }
}