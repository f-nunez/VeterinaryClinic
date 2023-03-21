using Microsoft.AspNetCore.Components;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification;

public partial class AppNotificationListItemComponent : ComponentBase
{
    [Inject]
    private IAppNotificationComponentService _appNotificationComponentService { get; set; }

    [Inject]
    private NavigationManager _navigationManager { get; set; }

    [Parameter]
    public AppNotificationListItem AppNotification { get; set; } = new();

    protected async void OnClickDelete()
    {
        await _appNotificationComponentService
            .DeleteAppNotificationAsync(AppNotification.Id);

        _appNotificationComponentService.RefreshAppNotificationList();
    }

    protected async void OnClickItem()
    {
        if (!AppNotification.IsRead)
        {
            await _appNotificationComponentService
                .MarkAppNotificationAsReadAsync(AppNotification.Id);

            AppNotification.IsRead = true;

            await InvokeAsync(StateHasChanged);
        }

        if (!string.IsNullOrEmpty(AppNotification.Url))
            _navigationManager.NavigateTo(AppNotification.Url);
    }
}