using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Settings;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification.Factories;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.GetAppNotifications;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification;

public partial class AppNotificationComponent : ComponentBase
{
    [Inject]
    private IAppNotificationBuilder _appNotificationBuilder { get; set; }

    [Inject]
    private IAppNotificationComponentService _appNotificationComponentService { get; set; }

    [Inject]
    private IBackendForFrontendSetting bffSetting { get; set; }

    private HubConnection _hubConnection { get; set; }

    private int _hubConnectionRetryCount { get; set; }

    [Inject]
    private ILogger<AppNotificationComponent> _logger { get; set; }

    [Inject]
    private NavigationManager _navigationManager { get; set; }

    protected List<AppNotificationListItem> AppNotifications { get; set; } = new();

    protected bool HasUnreadAppNotifications { get; set; }

    protected bool IsEmptyList { get; set; }

    protected bool IsFailedConnection { get; set; }

    protected bool IsLoading { get; set; }

    protected bool IsVisibleContainer { get; set; }

    protected bool IsVisibleList { get; set; }

    [Inject]
    protected IStringLocalizer<AppNotificationComponent> StringLocalizer { get; set; }

    protected int TotalAppNotifications { get; set; }

    protected override async void OnInitialized()
    {
        _appNotificationComponentService.OnHideContainer += HideContainer;
        _appNotificationComponentService.OnShowContainer += ShowContainer;

        await InitializeHubConnectionAsync();
    }

    protected async Task GetAppNotifications(GetAppNotificationsRequest request)
    {
        GetAppNotificationsResponse response = null;
        try
        {
            response = await _appNotificationComponentService.GetAppNotificationsAsync(request);

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
        HasUnreadAppNotifications = false;
        IsEmptyList = false;
        IsFailedConnection = false;
        IsLoading = true;
        IsVisibleContainer = true;
        IsVisibleList = true;
        StateHasChanged();
    }

    protected void OnClickFooter()
    {
        HideContainer();
    }

    private async Task InitializeHubConnectionAsync()
    {
        string reverseProxyRoute = bffSetting
            .SuffixRouteForSchedulingNotificationsHub;

        string token = await _appNotificationComponentService
            .GetAccessTokenAsync();

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(_navigationManager.ToAbsoluteUri(reverseProxyRoute), options =>
            {
                options.AccessTokenProvider = () => Task.FromResult(token);
            })
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.Closed += async (error) =>
        {
            _logger.LogError(error.Message, error);

            await Task.CompletedTask;
        };

        _hubConnection.On<string>("ReceiveAppNotification", async (message) =>
        {
            HasUnreadAppNotifications = true;

            await InvokeAsync(StateHasChanged);
        });

        try
        {
            await _hubConnection.StartAsync();

            await VerifyUnreadAppNotificationsAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            _logger.LogInformation($"RetryHubConnection: {++_hubConnectionRetryCount}");

            if (_hubConnectionRetryCount <= 3)
                await InitializeHubConnectionAsync();
        }
    }

    private async Task VerifyUnreadAppNotificationsAsync()
    {
        int unreadCount = 0;

        try
        {
            unreadCount = await _appNotificationComponentService
                .GetUnreadAppNotificationsCountAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }
        finally
        {
            HasUnreadAppNotifications = unreadCount > 0;
            await InvokeAsync(StateHasChanged);
        }
    }
}