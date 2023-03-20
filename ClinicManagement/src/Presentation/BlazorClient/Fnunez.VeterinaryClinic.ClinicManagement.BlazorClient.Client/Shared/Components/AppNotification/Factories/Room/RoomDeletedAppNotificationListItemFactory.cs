using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Factories;

public class RoomDeletedAppNotificationListItemFactory
    : BaseAppNotificationListItemFactory, IAppNotificationListItemFactory
{
    private RoomDeletedPayload? _payload;

    public RoomDeletedAppNotificationListItemFactory(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
        : base(appNotification)
    {
        _payload = GetPayload<RoomDeletedPayload>();
        SetMessage(GetMessageFromPayload(appNotification, stringLocalizer));
        SetModuleIcon("meeting_room");
        SetTitle(stringLocalizer["Event_RoomDeleted_Title"]);
        SetUrl(GetUrlFromPayload());
    }

    private string GetMessageFromPayload(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
    {
        if (_payload is null)
            return string.Empty;

        return string.Format(
            stringLocalizer["Event_RoomDeleted_Message"],
            _payload.Name,
            appNotification.TriggeredBy
        );
    }

    private string GetUrlFromPayload()
    {
        if (_payload is null)
            return string.Empty;

        return $"rooms/detail/{_payload.Id}";
    }
}