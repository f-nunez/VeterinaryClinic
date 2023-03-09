using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.DeleteAppNotification;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.MarkAppNotificationAsRead;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification;

public class AppNotificationComponentService : IAppNotificationComponentService
{
    private readonly IAppNotificationService _appNotificationService;
    public event Action? OnHideContainer;
    public event Action? OnShowContainer;
    public event Action? OnRefreshAppNotificationList;

    public AppNotificationComponentService(
        IAppNotificationService appNotificationService)
    {
        _appNotificationService = appNotificationService;
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

    public async Task DeleteAppNotificationAsync(Guid appNotificationId)
    {
        var request = new DeleteAppNotificationRequest
        {
            AppNotificationId = appNotificationId
        };

        await _appNotificationService.DeleteAppNotificationAsync(request);
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