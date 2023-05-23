namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;

public class ClinicDeletedIntegrationEvent : BaseIntegrationEvent
{
    public int ClinicId { get; set; }
}