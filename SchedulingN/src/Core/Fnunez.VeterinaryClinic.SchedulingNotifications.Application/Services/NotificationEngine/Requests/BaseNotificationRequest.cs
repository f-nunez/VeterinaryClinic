namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Requests;

public abstract class BaseNotificationRequest
{
    public Guid CorrelationId { get; set; }
    public string? TriggeredByUserId { get; set; }
}