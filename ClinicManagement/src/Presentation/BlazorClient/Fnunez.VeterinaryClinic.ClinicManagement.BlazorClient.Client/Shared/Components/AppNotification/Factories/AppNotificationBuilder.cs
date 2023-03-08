using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotifications;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Factories;

public class AppNotificationBuilder : IAppNotificationBuilder
{
    private IStringLocalizer<AppNotificationComponent> _stringLocalizer;
    private readonly IUserSettingsService _userSettingsService;

    public AppNotificationBuilder(
        IStringLocalizer<AppNotificationComponent> stringLocalizer,
        IUserSettingsService userSettingsService)
    {
        _stringLocalizer = stringLocalizer;
        _userSettingsService = userSettingsService;
    }

    public async Task<AppNotificationListItem> BuildAppNotificationListItemAsync(
        AppNotificationDto appNotification)
    {
        var notificationEvent = GetNotificationEvent(appNotification.Event);

        if (notificationEvent is null)
            return GetDefaultAppNotificationListItem(appNotification.Event);

        IAppNotificationListItemFactory? factory = null;

        switch (GetNotificationEvent(appNotification.Event))
        {
            case NotificationEvent.AppointmentTypeCreated:
                factory = new AppointmentTypeCreatedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.AppointmentTypeDeleted:
                factory = new AppointmentTypeDeletedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.AppointmentTypeUpdated:
                factory = new AppointmentTypeUpdatedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.ClientCreated:
                factory = new ClientCreatedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.ClientDeleted:
                factory = new ClientDeletedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.ClientUpdated:
                factory = new ClientUpdatedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.ClinicCreated:
                factory = new ClinicCreatedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.ClinicDeleted:
                factory = new ClinicDeletedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.ClinicUpdated:
                factory = new ClinicUpdatedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.DoctorCreated:
                factory = new DoctorCreatedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.DoctorDeleted:
                factory = new DoctorDeletedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.DoctorUpdated:
                factory = new DoctorUpdatedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.PatientCreated:
                factory = new PatientCreatedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.PatientDeleted:
                factory = new PatientDeletedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.PatientUpdated:
                factory = new PatientUpdatedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.RoomCreated:
                factory = new RoomCreatedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.RoomDeleted:
                factory = new RoomDeletedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.RoomUpdated:
                factory = new RoomUpdatedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            default:
                break;
        }

        if (factory != null)
            return factory.CreateAppNotificationListItem(
                await _userSettingsService.GetUtcOffsetInMinutesAsync());

        return GetDefaultAppNotificationListItem(appNotification.Event);
    }

    private NotificationEvent? GetNotificationEvent(
        string? notificationEventString)
    {
        bool isParsedNotificationEvent = Enum.TryParse(
            notificationEventString, out NotificationEvent notificationEvent);

        if (isParsedNotificationEvent)
            return notificationEvent;

        return null;
    }

    private AppNotificationListItem GetDefaultAppNotificationListItem(
        string? notificationEvent)
    {
        return new AppNotificationListItem
        {
            ModuleIcon = "contact_support",
            Message = notificationEvent,
        };
    }
}