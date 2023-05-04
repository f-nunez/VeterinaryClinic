namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;

public class ClientCreatedPayload : BasePayload
{
    public int Id { get; set; }
    public string? FullName { get; set; }
}