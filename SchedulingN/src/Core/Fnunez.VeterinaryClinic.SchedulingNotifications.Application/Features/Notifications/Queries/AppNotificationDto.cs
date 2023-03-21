namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Queries;

public class AppNotificationDto
{
    public DateTimeOffset CreatedOn { get; set; }
    public string? Event { get; set; }
    public Guid Id { get; set; }
    public bool IsRead { get; set; }
    public string? Payload { get; set; }
    public string? TriggeredBy { get; set; }
}