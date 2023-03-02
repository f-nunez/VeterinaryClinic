namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;

public class RoomUpdatedPayload : BasePayload
{
    public int Id { get; set; }
    public string? Name { get; set; }
}