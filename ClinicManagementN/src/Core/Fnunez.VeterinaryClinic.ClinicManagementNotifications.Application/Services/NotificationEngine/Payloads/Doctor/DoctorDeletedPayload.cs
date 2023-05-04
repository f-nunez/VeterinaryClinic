namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;

public class DoctorDeletedPayload : BasePayload
{
    public int Id { get; set; }
    public string? FullName { get; set; }
}