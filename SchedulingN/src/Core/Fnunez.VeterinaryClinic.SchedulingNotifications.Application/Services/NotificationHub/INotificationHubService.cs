namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationHub;

public interface INotificationHubService
{
    Task SendAppNotificationAsync(string? userId, string message);
}