using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Factories;

public class DoctorUpdatedAppNotificationListItemFactory
    : BaseAppNotificationListItemFactory, IAppNotificationListItemFactory
{
    private DoctorUpdatedPayload? _payload;

    public DoctorUpdatedAppNotificationListItemFactory(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
        : base(appNotification)
    {
        _payload = GetPayload<DoctorUpdatedPayload>();
        SetMessage(GetMessageFromPayload(appNotification, stringLocalizer));
        SetModuleIcon("face");
        SetTitle(stringLocalizer["Event_DoctorUpdated_Title"]);
        SetUrl(GetUrlFromPayload());
    }

    private string GetMessageFromPayload(
        AppNotificationDto appNotification,
        IStringLocalizer<AppNotificationComponent> stringLocalizer)
    {
        if (_payload is null)
            return string.Empty;

        return string.Format(
            stringLocalizer["Event_DoctorUpdated_Message"],
            _payload.FullName,
            appNotification.TriggeredBy
        );
    }

    private string GetUrlFromPayload()
    {
        if (_payload is null)
            return string.Empty;

        return $"doctors/detail/{_payload.Id}";
    }
}