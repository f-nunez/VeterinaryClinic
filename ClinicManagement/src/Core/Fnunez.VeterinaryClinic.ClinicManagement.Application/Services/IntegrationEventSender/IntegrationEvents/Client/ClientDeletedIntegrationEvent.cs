namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;

public class ClientDeletedIntegrationEvent : BaseIntegrationEvent
{
    public int ClientId { get; set; }
}