using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.AppNotificationAggregate;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services;

public interface INotificationEngineService
{
    public Task<List<AppNotification>> CreateAndNotifyAsync(string notificationEventString, string serializedNotificationRequest, CancellationToken cancellationToken);
}