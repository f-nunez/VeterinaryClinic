namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Requests;

public class AppointmentConfirmedNotificationRequest : BaseNotificationRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
}