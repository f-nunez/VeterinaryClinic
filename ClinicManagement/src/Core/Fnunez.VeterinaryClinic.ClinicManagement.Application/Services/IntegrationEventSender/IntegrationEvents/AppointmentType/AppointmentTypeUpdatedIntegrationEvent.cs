namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;

public class AppointmentTypeUpdatedIntegrationEvent : BaseIntegrationEvent
{
    public string AppointmentTypeCode { get; set; } = string.Empty;
    public int AppointmentTypeDuration { get; set; }
    public int AppointmentTypeId { get; set; }
    public string AppointmentTypeName { get; set; } = string.Empty;
}