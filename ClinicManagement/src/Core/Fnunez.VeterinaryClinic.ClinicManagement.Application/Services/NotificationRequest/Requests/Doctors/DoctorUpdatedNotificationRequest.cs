namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;

public class DoctorUpdatedNotificationRequest : BaseNotificationRequest
{
    public int Id { get; set; }
    public string? FullName { get; set; }
}