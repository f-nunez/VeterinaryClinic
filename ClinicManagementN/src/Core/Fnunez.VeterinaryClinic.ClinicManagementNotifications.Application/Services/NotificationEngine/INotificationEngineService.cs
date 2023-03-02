using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.AppNotificationAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services;

public interface INotificationEngineService
{
    public Task<List<AppNotification>> CreateAndNotifyAsync(string notificationEventString, string serializedNotificationRequest);
}