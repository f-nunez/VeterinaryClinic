using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotifications;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification;

public partial class AppNotificationListComponent : ComponentBase
{
    [Inject]
    private IAppNotificationComponentService _appNotificationComponentService { get; set; }

    protected Virtualize<AppNotificationListItem> AppNotificationList { get; set; }

    [Inject]
    protected IStringLocalizer<AppNotificationComponent> StringLocalizer { get; set; }

    [Parameter]
    public List<AppNotificationListItem> AppNotifications { get; set; } = new();

    [Parameter]
    public EventCallback<GetAppNotificationsRequest> OnScroll { get; set; }

    [Parameter]
    public int TotalAppNotifications { get; set; }

    protected override void OnInitialized()
    {
        _appNotificationComponentService.OnRefreshAppNotificationList += RefreshAppNotificationList;
    }

    protected async ValueTask<ItemsProviderResult<AppNotificationListItem>> LoadAppNotifications(
        ItemsProviderRequest request)
    {
        int itemsToBeProvided = Math.Min(
            request.Count,
            TotalAppNotifications - request.StartIndex
        );

        await OnScroll.InvokeAsync(new GetAppNotificationsRequest
        {
            Skip = request.StartIndex,
            Take = itemsToBeProvided > 0 ? itemsToBeProvided : request.Count
        });

        return new ItemsProviderResult<AppNotificationListItem>(
            AppNotifications, TotalAppNotifications);
    }

    public async void RefreshAppNotificationList()
    {
        await AppNotificationList.RefreshDataAsync();

        await InvokeAsync(StateHasChanged);
    }
}