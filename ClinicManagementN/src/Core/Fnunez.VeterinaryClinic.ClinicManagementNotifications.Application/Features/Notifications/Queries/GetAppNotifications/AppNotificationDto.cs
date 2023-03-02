namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries.GetAppNotifications;

public class AppNotificationDto
{
    public DateTimeOffset CreatedOn { get; set; }
    public string? Event { get; set; }
    public Guid Id { get; set; }
    public bool IsRead { get; set; }
    public string? Payload { get; set; }
    public string? TriggeredBy { get; set; }
}