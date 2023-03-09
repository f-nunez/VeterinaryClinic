using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotifications;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification;

public partial class AppNotificationComponent : ComponentBase
{
    [Inject]
    private IAppNotificationBuilder _appNotificationBuilder { get; set; }

    [Inject]
    private IAppNotificationComponentService _appNotificationComponentService { get; set; }

    [Inject]
    private IAppNotificationService _appNotificationService { get; set; }

    protected List<AppNotificationListItem> AppNotifications { get; set; } = new();

    protected bool IsVisibleContainer { get; set; }

    [Inject]
    protected IStringLocalizer<AppNotificationComponent> StringLocalizer { get; set; }

    protected int TotalAppNotifications { get; set; }

    protected override void OnInitialized()
    {
        _appNotificationComponentService.OnHideContainer += HideContainer;
        _appNotificationComponentService.OnShowContainer += ShowContainer;
    }

    protected async Task GetAppNotifications(GetAppNotificationsRequest request)
    {
        var response = await _appNotificationService
            .GetAppNotificationsAsync(request);

        var listItems = new List<AppNotificationListItem>();

        foreach (var appNotification in response.AppNotifications)
            listItems.Add(await _appNotificationBuilder.BuildAppNotificationListItemAsync(appNotification));

        AppNotifications = listItems;

        TotalAppNotifications = response.Count;
    }

    protected void HideContainer()
    {
        IsVisibleContainer = false;
    }

    protected void ShowContainer()
    {
        IsVisibleContainer = true;
    }
}