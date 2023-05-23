namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;

public class DoctorUpdatedIntegrationEvent : BaseIntegrationEvent
{
    public string DoctorFullName { get; set; } = string.Empty;
    public int DoctorId { get; set; }
}