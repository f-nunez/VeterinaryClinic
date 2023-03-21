using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Notifications.Commands.DeleteAppNotifications;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification.Factories;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.NotificationCenter;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.DeleteAppNotification;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.GetAppNotificationsDataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.NotificationCenter;

public partial class NotificationCenterComponent : ComponentBase
{
    [Inject]
    private IAppNotificationBuilder _appNotificationBuilder { get; set; }

    [Inject]
    private IAppNotificationService _appNotificationService { get; set; }

    [Inject]
    private DialogService _dialogService { get; set; }

    protected bool EnabledDeleteAllButton { get; set; } = true;

    protected NotificationCenterFilterValue FilterValue { get; set; }

    protected RadzenDataGrid<NotificationCenterItemVm> NotificationCenterDataGrid;

    protected List<NotificationCenterItemVm> NotificationCenterDataGridItems;

    protected int Count;

    [Inject]
    protected IStringLocalizer<NotificationCenterComponent> StringLocalizer { get; set; }

    protected bool IsLoading = false;

    protected IEnumerable<int> PageSizeOptions = new int[] { 5, 10, 20, 30, 50, 100 };

    protected async Task LoadData(LoadDataArgs args)
    {
        IsLoading = true;

        var request = new GetAppNotificationsDataGridRequest
        {
            DataGridRequest = args.GetDataGridRequest(),
            OnlyReadFilterValue = FilterValue == NotificationCenterFilterValue.OnlyRead,
            OnlyUnreadFilterValue = FilterValue == NotificationCenterFilterValue.OnlyUnread
        };

        var dataGridResponse = await _appNotificationService
            .GetAppNotificationsDataGridAsync(request);

        var appNotificationDtos = dataGridResponse.Items;

        List<NotificationCenterItemVm> notifications = new();

        foreach (var appNotification in appNotificationDtos)
        {
            var appNotificationListItem = await _appNotificationBuilder
                .BuildAppNotificationListItemAsync(appNotification);

            var notification = NotificationCenterHelper
                .MapNotificationCenterItemViewModel(appNotificationListItem);

            notifications.Add(notification);
        }

        Count = dataGridResponse.Count;

        NotificationCenterDataGridItems = notifications;

        IsLoading = false;

        EnabledDeleteAllButton = notifications.Any();

        await InvokeAsync(StateHasChanged);
    }

    protected async Task ResetGrid()
    {
        NotificationCenterDataGrid.Reset(false);
        await NotificationCenterDataGrid.FirstPage(true);
    }

    protected async Task OnClickDelete(NotificationCenterItemVm item)
    {
        bool? proceedToDelete = await _dialogService.Confirm(
            StringLocalizer["NotificationCenter_DeleteNotification_Alert_Message"],
            StringLocalizer["NotificationCenter_DeleteNotification_Alert_Title"],
            new ConfirmOptions
            {
                OkButtonText = StringLocalizer["NotificationCenter_DeleteNotification_Alert_Button_Ok"],
                CancelButtonText = StringLocalizer["NotificationCenter_DeleteNotification_Alert_Button_Cancel"]
            }
        );

        if (!proceedToDelete.HasValue || !proceedToDelete.Value)
            return;

        var request = new DeleteAppNotificationRequest
        {
            AppNotificationId = item.Id
        };

        await _appNotificationService.DeleteAppNotificationAsync(request);

        await ShowAlertAsync(
            StringLocalizer["NotificationCenter_DeletedNotification_Alert_Message"],
            StringLocalizer["NotificationCenter_DeletedNotification_Alert_Title"],
            StringLocalizer["NotificationCenter_DeletedNotification_Alert_Button_Ok"]);

        await NotificationCenterDataGrid.ReloadAfterDeleteItemAsync();
    }

    protected async Task OnClickDeleteAll()
    {
        bool? proceedToDelete = await _dialogService.Confirm(
            StringLocalizer["NotificationCenter_DeleteAllNotification_Alert_Message"],
            StringLocalizer["NotificationCenter_DeleteAllNotification_Alert_Title"],
            new ConfirmOptions
            {
                OkButtonText = StringLocalizer["NotificationCenter_DeleteAllNotification_Alert_Button_Ok"],
                CancelButtonText = StringLocalizer["NotificationCenter_DeleteAllNotification_Alert_Button_Cancel"]
            }
        );

        if (!proceedToDelete.HasValue || !proceedToDelete.Value)
            return;

        var appNotificationIds = NotificationCenterDataGridItems
            .Select(x => x.Id)
            .ToList();

        var request = new DeleteAppNotificationsRequest
        {
            AppNotificationIds = appNotificationIds
        };

        await _appNotificationService.DeleteAppNotificationsAsync(request);

        await ShowAlertAsync(
            StringLocalizer["NotificationCenter_DeletedAllNotification_Alert_Message"],
            StringLocalizer["NotificationCenter_DeletedAllNotification_Alert_Title"],
            StringLocalizer["NotificationCenter_DeletedAllNotification_Alert_Button_Ok"]);

        await NotificationCenterDataGrid.ReloadAfterDeletePageAsync();
    }

    private async Task<bool?> ShowAlertAsync(
        string alertMessage,
        string alertTitle,
        string alertButtonOkMessage)
    {
        return await _dialogService.Alert(
            alertMessage,
            alertTitle,
            new AlertOptions
            {
                OkButtonText = alertButtonOkMessage
            }
        );
    }
}