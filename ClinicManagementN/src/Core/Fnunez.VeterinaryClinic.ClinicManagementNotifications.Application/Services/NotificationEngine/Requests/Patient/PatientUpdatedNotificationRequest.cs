namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;

public class PatientUpdatedNotificationRequest : BaseNotificationRequest
{
    public int ClientId { get; set; }
    public int PatientId { get; set; }
    public string? Name { get; set; }
}