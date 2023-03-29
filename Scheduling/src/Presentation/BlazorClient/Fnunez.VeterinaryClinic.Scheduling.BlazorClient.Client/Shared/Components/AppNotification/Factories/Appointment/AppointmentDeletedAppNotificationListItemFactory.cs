using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification.Payloads;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification.Factories;

public class AppointmentDeletedAppNotificationListItemFactory
    : BaseAppNotificationListItemFactory, IAppNotificationListItemFactory
{
    private AppointmentDeletedPayload? _payload;

    public AppointmentDeletedAppNotificationListItemFactory(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
        : base(appNotification)
    {
        _payload = GetPayload<AppointmentDeletedPayload>();
        SetMessage(GetMessageFromPayload(appNotification, stringLocalizer));
        SetModuleIcon("event");
        SetTitle(stringLocalizer["Event_AppointmentDeleted_Title"]);
        SetUrl(GetUrlFromPayload());
    }

    private string GetMessageFromPayload(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
    {
        if (_payload is null)
            return string.Empty;

        return string.Format(
            stringLocalizer["Event_AppointmentDeleted_Message"],
            _payload.Title,
            appNotification.TriggeredBy
        );
    }

    private string GetUrlFromPayload()
    {
        if (_payload is null)
            return string.Empty;

        return $"appointments/detail/{_payload.Id}";
    }
}