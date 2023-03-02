namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;

public class RoomDeletedPayload : BasePayload
{
    public int Id { get; set; }
    public string? Name { get; set; }
}