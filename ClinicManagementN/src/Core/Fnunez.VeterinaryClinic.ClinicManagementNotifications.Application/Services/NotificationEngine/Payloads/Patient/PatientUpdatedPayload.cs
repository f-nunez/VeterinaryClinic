namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;

public class PatientUpdatedPayload : BasePayload
{
    public int Id { get; set; }
    public string? Name { get; set; }
}