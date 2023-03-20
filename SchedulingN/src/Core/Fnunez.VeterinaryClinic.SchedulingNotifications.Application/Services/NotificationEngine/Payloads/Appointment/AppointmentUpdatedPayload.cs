namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Payloads;

public class AppointmentUpdatedPayload : BasePayload
{
    public int Id { get; set; }
    public string? Title { get; set; }
}