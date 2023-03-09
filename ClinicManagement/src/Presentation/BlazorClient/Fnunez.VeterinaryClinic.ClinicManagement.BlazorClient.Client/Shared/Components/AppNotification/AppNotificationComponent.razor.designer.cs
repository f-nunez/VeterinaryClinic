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

    [Inject]
    private ILogger<AppNotificationComponent> _logger { get; set; }

    protected List<AppNotificationListItem> AppNotifications { get; set; } = new();

    protected bool IsEmptyList { get; set; }

    protected bool IsFailedConnection { get; set; }

    protected bool IsLoading { get; set; }

    protected bool IsVisibleContainer { get; set; }

    protected bool IsVisibleList { get; set; }

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
        GetAppNotificationsResponse response = null;
        try
        {
            response = await _appNotificationService.GetAppNotificationsAsync(request);

            var listItems = new List<AppNotificationListItem>();

            foreach (var appNotification in response.AppNotifications)
                listItems.Add(await _appNotificationBuilder.BuildAppNotificationListItemAsync(appNotification));

            AppNotifications = listItems;

            TotalAppNotifications = response.Count;

            IsEmptyList = TotalAppNotifications == 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            IsVisibleList = false;
            IsFailedConnection = true;
        }
        finally
        {
            IsLoading = false;
        }
    }

    protected void HideContainer()
    {
        IsVisibleContainer = false;
    }

    protected void ShowContainer()
    {
        IsEmptyList = false;
        IsFailedConnection = false;
        IsLoading = true;
        IsVisibleContainer = true;
        IsVisibleList = true;
    }
}