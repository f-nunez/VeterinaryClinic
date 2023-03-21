namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;

public class PatientCreatedPayload : BasePayload
{
    public int ClientId { get; set; }
    public int PatientId { get; set; }
    public string? Name { get; set; }
}