namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;

public class DoctorCreatedIntegrationEvent : BaseIntegrationEvent
{
    public string DoctorFullName { get; set; } = string.Empty;
    public int DoctorId { get; set; }
}