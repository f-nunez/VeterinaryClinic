namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;

public class AppointmentTypeDeletedIntegrationEvent : BaseIntegrationEvent
{
    public int AppointmentTypeId { get; set; }
}