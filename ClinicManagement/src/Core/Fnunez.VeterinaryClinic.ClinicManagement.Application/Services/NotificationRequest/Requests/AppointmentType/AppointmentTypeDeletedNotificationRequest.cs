namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;

public class AppointmentTypeDeletedNotificationRequest
    : BaseNotificationRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
}