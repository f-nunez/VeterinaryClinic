namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;

public abstract class BaseNotificationRequest
{
    public Guid CorrelationId { get; set; }
    public string? TriggeredByUserId { get; set; }
}