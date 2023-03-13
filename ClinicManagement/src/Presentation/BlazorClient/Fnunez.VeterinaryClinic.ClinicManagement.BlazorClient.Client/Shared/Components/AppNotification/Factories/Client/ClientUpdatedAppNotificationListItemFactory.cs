using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotifications;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Factories;

public class ClientUpdatedAppNotificationListItemFactory
    : BaseAppNotificationListItemFactory, IAppNotificationListItemFactory
{
    private ClientUpdatedPayload? _payload;

    public ClientUpdatedAppNotificationListItemFactory(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
        : base(appNotification)
    {
        _payload = GetPayload<ClientUpdatedPayload>();
        SetMessage(GetMessageFromPayload(appNotification, stringLocalizer));
        SetModuleIcon("people_alt");
        SetTitle(stringLocalizer["Event_ClientUpdated_Title"]);
        SetUrl(GetUrlFromPayload());
    }

    private string GetMessageFromPayload(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
    {
        if (_payload is null)
            return string.Empty;

        return string.Format(
            stringLocalizer["Event_ClientUpdated_Message"],
            _payload.FullName,
            appNotification.TriggeredBy
        );
    }

    private string GetUrlFromPayload()
    {
        if (_payload is null)
            return string.Empty;

        return $"clients/detail/{_payload.Id}";
    }
}