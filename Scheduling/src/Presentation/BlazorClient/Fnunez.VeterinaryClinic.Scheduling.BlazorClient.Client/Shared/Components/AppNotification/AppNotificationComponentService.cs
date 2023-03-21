using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.DeleteAppNotification;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.GetAppNotifications;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.GetUnreadAppNotificationsCount;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.MarkAppNotificationAsRead;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification;

public class AppNotificationComponentService : IAppNotificationComponentService
{
    private readonly IAppNotificationService _appNotificationService;
    private readonly ISecurityService _securityService;
    public event Action? OnHideContainer;
    public event Action? OnShowContainer;
    public event Action? OnRefreshAppNotificationList;

    public AppNotificationComponentService(
        IAppNotificationService appNotificationService,
        ISecurityService securityService)
    {
        _appNotificationService = appNotificationService;
        _securityService = securityService;
    }

    public void HideContainer()
    {
        OnHideContainer?.Invoke();
    }

    public void ShowContainer()
    {
        OnShowContainer?.Invoke();
    }

    public void RefreshAppNotificationList()
    {
        OnRefreshAppNotificationList?.Invoke();
    }

    public async Task<string> GetAccessTokenAsync()
    {
        return await _securityService.GetAccessTokenAsync();
    }

    public async Task DeleteAppNotificationAsync(Guid appNotificationId)
    {
        var request = new DeleteAppNotificationRequest
        {
            AppNotificationId = appNotificationId
        };

        await _appNotificationService.DeleteAppNotificationAsync(request);
    }

    public async Task<GetAppNotificationsResponse> GetAppNotificationsAsync(
        GetAppNotificationsRequest request)
    {
        return await _appNotificationService.GetAppNotificationsAsync(request);
    }

    public async Task<int> GetUnreadAppNotificationsCountAsync()
    {
        var request = new GetUnreadAppNotificationsCountRequest();

        var count = await _appNotificationService
            .GetUnreadAppNotificationsCountAsync(request);

        return count;
    }

    public async Task MarkAppNotificationAsReadAsync(Guid appNotificationId)
    {
        var request = new MarkAppNotificationAsReadRequest
        {
            AppNotificationId = appNotificationId
        };

        await _appNotificationService.MarkAppNotificationAsReadAsync(request);
    }
}