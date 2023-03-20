using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Requests;

public interface INotificationRequestFactory
{
    public BaseNotificationRequest GetNotificationRequest(NotificationEvent notificationEvent, string serializedNotificationRequest);
}