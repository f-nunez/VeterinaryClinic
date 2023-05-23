namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;

public class AppointmentTypeDeletedIntegrationEvent : BaseIntegrationEvent
{
    public int AppointmentTypeId { get; set; }
}