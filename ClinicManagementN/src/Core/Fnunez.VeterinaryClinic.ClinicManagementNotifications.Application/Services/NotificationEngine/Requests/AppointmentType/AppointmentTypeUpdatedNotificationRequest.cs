namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;

public class AppointmentTypeUpdatedNotificationRequest
    : BaseNotificationRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
}