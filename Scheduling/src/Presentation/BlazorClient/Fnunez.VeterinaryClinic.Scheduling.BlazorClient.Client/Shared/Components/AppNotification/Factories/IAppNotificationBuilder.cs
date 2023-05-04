using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification.Factories;

public interface IAppNotificationBuilder
{
    Task<AppNotificationListItem> BuildAppNotificationListItemAsync(AppNotificationDto appNotification);
}