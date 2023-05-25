namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;

public class DoctorDeletedIntegrationEvent : BaseIntegrationEvent
{
    public int DoctorId { get; set; }
}