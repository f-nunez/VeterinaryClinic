namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Requests;

public class AppointmentDeletedNotificationRequest : BaseNotificationRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
}