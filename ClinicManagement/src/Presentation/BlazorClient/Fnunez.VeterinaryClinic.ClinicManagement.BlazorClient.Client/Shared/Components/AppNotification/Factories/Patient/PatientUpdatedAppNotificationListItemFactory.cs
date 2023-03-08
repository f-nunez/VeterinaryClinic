using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotifications;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Factories;

public class PatientUpdatedAppNotificationListItemFactory
    : BaseAppNotificationListItemFactory, IAppNotificationListItemFactory
{
    public PatientUpdatedAppNotificationListItemFactory(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
        : base(appNotification)
    {
        SetMessage(GetMessageFromPayload(appNotification, stringLocalizer));
        SetModuleIcon("pets");
        SetTitle(stringLocalizer["Event_PatientUpdated_Title"]);
    }

    private string GetMessageFromPayload(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
    {
        var payload = GetPayload<PatientUpdatedPayload>();

        if (payload is null)
            return string.Empty;

        return string.Format(
            stringLocalizer["Event_PatientUpdated_Message"],
            payload.Name,
            appNotification.TriggeredBy
        );
    }
}