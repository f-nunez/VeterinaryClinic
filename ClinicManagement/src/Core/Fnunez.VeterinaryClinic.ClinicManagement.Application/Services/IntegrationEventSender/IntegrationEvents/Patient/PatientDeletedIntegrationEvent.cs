namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;

public class PatientDeletedIntegrationEvent : BaseIntegrationEvent
{
    public int PatientClientId { get; set; }
    public int PatientId { get; set; }
}