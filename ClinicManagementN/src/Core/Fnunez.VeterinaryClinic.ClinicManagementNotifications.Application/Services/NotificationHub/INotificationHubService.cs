namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationHub;

public interface INotificationHubService
{
    Task SendAppNotificationAsync(string? userId, string message);
}