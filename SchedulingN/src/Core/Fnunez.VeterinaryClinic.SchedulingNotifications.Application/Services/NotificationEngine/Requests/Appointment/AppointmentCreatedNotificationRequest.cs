namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Requests;

public class AppointmentCreatedNotificationRequest
    : BaseNotificationRequest
{
    public int Id { get; set; }
    public string? Title { get; set; }
}