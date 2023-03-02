namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;

public class ClientDeletedNotificationRequest : BaseNotificationRequest
{
    public int Id { get; set; }
    public string? FullName { get; set; }
}