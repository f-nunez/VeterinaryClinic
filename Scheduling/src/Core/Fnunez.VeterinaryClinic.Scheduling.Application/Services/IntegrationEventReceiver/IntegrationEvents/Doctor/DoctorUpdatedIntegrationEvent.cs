namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;

public class DoctorUpdatedIntegrationEvent : BaseIntegrationEvent
{
    public string DoctorFullName { get; set; } = string.Empty;
    public int DoctorId { get; set; }
}