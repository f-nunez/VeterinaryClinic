namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;

public class DoctorCreatedNotificationRequest : BaseNotificationRequest
{
    public int Id { get; set; }
    public string? FullName { get; set; }
}