using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification.Payloads;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification.Factories;

public class AppointmentUpdatedAppNotificationListItemFactory
    : BaseAppNotificationListItemFactory, IAppNotificationListItemFactory
{
    private AppointmentUpdatedPayload? _payload;

    public AppointmentUpdatedAppNotificationListItemFactory(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
        : base(appNotification)
    {
        _payload = GetPayload<AppointmentUpdatedPayload>();
        SetMessage(GetMessageFromPayload(appNotification, stringLocalizer));
        SetModuleIcon("event");
        SetTitle(stringLocalizer["Event_AppointmentUpdated_Title"]);
        SetUrl(GetUrlFromPayload());
    }

    private string GetMessageFromPayload(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
    {
        if (_payload is null)
            return string.Empty;

        return string.Format(
            stringLocalizer["Event_AppointmentUpdated_Message"],
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