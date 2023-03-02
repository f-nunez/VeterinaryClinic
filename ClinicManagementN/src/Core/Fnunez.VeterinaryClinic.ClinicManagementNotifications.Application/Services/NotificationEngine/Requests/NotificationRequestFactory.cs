using System.Text.Json;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.NotificationAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;

public class NotificationRequestFactory : INotificationRequestFactory
{
    public BaseNotificationRequest GetNotificationRequest(
        NotificationEvent notificationEvent,
        string serializedNotificationRequest)
    {
        BaseNotificationRequest? notificationRequest;

        switch (notificationEvent)
        {
            case NotificationEvent.AppointmentTypeCreated:
                notificationRequest = JsonSerializer.Deserialize<AppointmentTypeCreatedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.AppointmentTypeDeleted:
                notificationRequest = JsonSerializer.Deserialize<AppointmentTypeDeletedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.AppointmentTypeUpdated:
                notificationRequest = JsonSerializer.Deserialize<AppointmentTypeUpdatedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.ClientCreated:
                notificationRequest = JsonSerializer.Deserialize<ClientCreatedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.ClientDeleted:
                notificationRequest = JsonSerializer.Deserialize<ClientDeletedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.ClientUpdated:
                notificationRequest = JsonSerializer.Deserialize<ClientUpdatedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.ClinicCreated:
                notificationRequest = JsonSerializer.Deserialize<ClinicCreatedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.ClinicDeleted:
                notificationRequest = JsonSerializer.Deserialize<ClinicDeletedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.ClinicUpdated:
                notificationRequest = JsonSerializer.Deserialize<ClinicUpdatedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.DoctorCreated:
                notificationRequest = JsonSerializer.Deserialize<DoctorCreatedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.DoctorDeleted:
                notificationRequest = JsonSerializer.Deserialize<DoctorDeletedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.DoctorUpdated:
                notificationRequest = JsonSerializer.Deserialize<DoctorUpdatedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.PatientCreated:
                notificationRequest = JsonSerializer.Deserialize<PatientCreatedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.PatientDeleted:
                notificationRequest = JsonSerializer.Deserialize<PatientDeletedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.PatientUpdated:
                notificationRequest = JsonSerializer.Deserialize<PatientUpdatedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.RoomCreated:
                notificationRequest = JsonSerializer.Deserialize<RoomCreatedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.RoomDeleted:
                notificationRequest = JsonSerializer.Deserialize<RoomDeletedNotificationRequest>(serializedNotificationRequest);
                break;
            case NotificationEvent.RoomUpdated:
                notificationRequest = JsonSerializer.Deserialize<RoomUpdatedNotificationRequest>(serializedNotificationRequest);
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