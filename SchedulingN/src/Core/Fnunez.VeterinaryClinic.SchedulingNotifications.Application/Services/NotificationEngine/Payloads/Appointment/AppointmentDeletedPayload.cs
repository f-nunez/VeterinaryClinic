namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Payloads;

public class AppointmentDeletedPayload : BasePayload
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
}