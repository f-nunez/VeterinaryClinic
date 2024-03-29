using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification;

public interface IAppNotificationComponentService
{
    event Action? OnHideContainer;
    event Action? OnShowContainer;
    event Action? OnRefreshAppNotificationList;
    void HideContainer();
    void ShowContainer();
    void RefreshAppNotificationList();
    Task DeleteAppNotificationAsync(Guid appNotificationId);
    Task<string> GetAccessTokenAsync();
    Task<GetAppNotificationsResponse> GetAppNotificationsAsync(GetAppNotificationsRequest request);
    Task<int> GetUnreadAppNotificationsCountAsync();
    Task MarkAppNotificationAsReadAsync(Guid appNotificationId);
}