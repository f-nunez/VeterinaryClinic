namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;

public class PatientDeletedPayload : BasePayload
{
    public int Id { get; set; }
    public string? Name { get; set; }
}