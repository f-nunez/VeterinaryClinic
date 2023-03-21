namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification.Factories;

public interface IAppNotificationListItemFactory
{
    AppNotificationListItem CreateAppNotificationListItem(int timezoneOffset);
}