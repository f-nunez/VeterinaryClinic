using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.NotificationAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;

public interface INotificationRequestFactory
{
    public BaseNotificationRequest GetNotificationRequest(NotificationEvent notificationEvent, string serializedNotificationRequest);
}