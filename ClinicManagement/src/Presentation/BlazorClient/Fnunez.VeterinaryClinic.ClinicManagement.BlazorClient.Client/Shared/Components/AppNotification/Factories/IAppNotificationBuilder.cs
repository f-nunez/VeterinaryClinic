using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Factories;

public interface IAppNotificationBuilder
{
    Task<AppNotificationListItem> BuildAppNotificationListItemAsync(AppNotificationDto appNotification);
}