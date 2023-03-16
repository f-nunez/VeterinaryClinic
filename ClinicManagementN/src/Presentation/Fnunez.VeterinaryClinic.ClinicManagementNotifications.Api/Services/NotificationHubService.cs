using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Api.Hubs;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationHub;
using Microsoft.AspNetCore.SignalR;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Api.Services;

public class NotificationHubService : INotificationHubService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationHubService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendAppNotificationAsync(string? userId, string message)
    {
        if (string.IsNullOrEmpty(userId))
            return;

        await _hubContext.Clients.User(userId)
            .SendAsync("ReceiveAppNotification", message);
    }
}