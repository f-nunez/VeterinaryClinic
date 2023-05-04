using System.Text.Json;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Requests;

public class NotificationRequestFactory : INotificationRequestFactory
{
    public BaseNotificationRequest GetNotificationRequest(
        NotificationEvent notificationEvent,
        string serializedNotificationRequest)
    {
        BaseNotificationRequest? notificationRequest;

        switch (notificationEvent)
        {
            case NotificationEvent.AppointmentConfirmed:
                notificationRequest = JsonSerializer.Deserialize<AppointmentConfirmedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.AppointmentCreated:
                notificationRequest = JsonSerializer.Deserialize<AppointmentCreatedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.AppointmentDeleted:
                notificationRequest = JsonSerializer.Deserialize<AppointmentDeletedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.AppointmentUpdated:
                notificationRequest = JsonSerializer.Deserialize<AppointmentUpdatedNotificationRequest>(serializedNotificationRequest);
                break;
            default:
                throw new ArgumentException(
                    $"{nameof(notificationEvent)} not found with value: {notificationEvent}");
        }

        if (notificationRequest is null)
            throw new ArgumentNullException(nameof(notificationRequest));

        return notificationRequest;
    }
}