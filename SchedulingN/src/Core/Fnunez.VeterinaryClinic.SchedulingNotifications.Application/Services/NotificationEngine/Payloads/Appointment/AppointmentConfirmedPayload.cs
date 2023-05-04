namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Payloads;

public class AppointmentConfirmedPayload : BasePayload
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
}