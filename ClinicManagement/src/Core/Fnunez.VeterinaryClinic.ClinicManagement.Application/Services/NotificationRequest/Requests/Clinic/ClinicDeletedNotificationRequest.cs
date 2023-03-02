namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;

public class ClinicDeletedNotificationRequest : BaseNotificationRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
}