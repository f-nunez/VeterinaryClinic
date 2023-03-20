namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Requests;

public class AppointmentUpdatedNotificationRequest
    : BaseNotificationRequest
{
    public int Id { get; set; }
    public string? Title { get; set; }
}