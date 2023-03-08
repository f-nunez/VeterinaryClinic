using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotifications;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Factories;

public class AppointmentTypeDeletedAppNotificationListItemFactory
    : BaseAppNotificationListItemFactory, IAppNotificationListItemFactory
{
    public AppointmentTypeDeletedAppNotificationListItemFactory(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
        : base(appNotification)
    {
        SetMessage(GetMessageFromPayload(appNotification, stringLocalizer));
        SetModuleIcon("widgets");
        SetTitle(stringLocalizer["Event_AppointmentTypeDeleted_Title"]);
    }

    private string GetMessageFromPayload(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
    {
        var payload = GetPayload<AppointmentTypeDeletedPayload>();

        if (payload is null)
            return string.Empty;

        return string.Format(
            stringLocalizer["Event_AppointmentTypeDeleted_Message"],
            payload.Name,
            appNotification.TriggeredBy
        );
    }
}