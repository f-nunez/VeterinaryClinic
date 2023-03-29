using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.GetAppNotifications;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification.Factories;

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
            case NotificationEvent.AppointmentConfirmed:
                factory = new AppointmentConfirmedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.AppointmentCreated:
                factory = new AppointmentCreatedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.AppointmentDeleted:
                factory = new AppointmentDeletedAppNotificationListItemFactory(appNotification, _stringLocalizer);
                break;
            case NotificationEvent.AppointmentUpdated:
                factory = new AppointmentUpdatedAppNotificationListItemFactory(appNotification, _stringLocalizer);
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