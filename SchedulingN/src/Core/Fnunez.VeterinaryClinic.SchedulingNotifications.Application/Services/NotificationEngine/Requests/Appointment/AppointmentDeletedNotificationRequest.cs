namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Requests;

public class AppointmentDeletedNotificationRequest
    : BaseNotificationRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
}