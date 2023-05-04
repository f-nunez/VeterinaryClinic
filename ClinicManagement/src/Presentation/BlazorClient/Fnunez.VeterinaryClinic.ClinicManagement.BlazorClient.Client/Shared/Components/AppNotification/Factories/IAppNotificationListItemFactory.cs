namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Factories;

public interface IAppNotificationListItemFactory
{
    AppNotificationListItem CreateAppNotificationListItem(int timezoneOffset);
}