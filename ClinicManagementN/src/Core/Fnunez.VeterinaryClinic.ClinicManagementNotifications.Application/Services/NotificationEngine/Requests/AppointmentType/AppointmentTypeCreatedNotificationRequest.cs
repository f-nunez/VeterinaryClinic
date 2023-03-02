namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;

public class AppointmentTypeCreatedNotificationRequest
    : BaseNotificationRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
}