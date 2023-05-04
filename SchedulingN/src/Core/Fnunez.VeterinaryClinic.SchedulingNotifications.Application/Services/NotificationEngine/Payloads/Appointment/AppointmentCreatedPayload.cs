namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Payloads;

public class AppointmentCreatedPayload : BasePayload
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
}