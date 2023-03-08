using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotifications;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Factories;

public class PatientCreatedAppNotificationListItemFactory
    : BaseAppNotificationListItemFactory, IAppNotificationListItemFactory
{
    public PatientCreatedAppNotificationListItemFactory(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
        : base(appNotification)
    {
        SetMessage(GetMessageFromPayload(appNotification, stringLocalizer));
        SetModuleIcon("pets");
        SetTitle(stringLocalizer["Event_PatientCreated_Title"]);
    }

    private string GetMessageFromPayload(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
    {
        var payload = GetPayload<PatientCreatedPayload>();

        if (payload is null)
            return string.Empty;

        return string.Format(
            stringLocalizer["Event_PatientCreated_Message"],
            payload.Name,
            appNotification.TriggeredBy
        );
    }
}