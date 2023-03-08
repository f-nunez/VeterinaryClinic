using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotifications;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Factories;

public class DoctorUpdatedAppNotificationListItemFactory
    : BaseAppNotificationListItemFactory, IAppNotificationListItemFactory
{
    public DoctorUpdatedAppNotificationListItemFactory(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
        : base(appNotification)
    {
        SetMessage(GetMessageFromPayload(appNotification, stringLocalizer));
        SetModuleIcon("face");
        SetTitle(stringLocalizer["Event_DoctorUpdated_Title"]);
    }

    private string GetMessageFromPayload(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
    {
        var payload = GetPayload<DoctorUpdatedPayload>();

        if (payload is null)
            return string.Empty;

        return string.Format(
            stringLocalizer["Event_DoctorUpdated_Message"],
            payload.FullName,
            appNotification.TriggeredBy
        );
    }
}