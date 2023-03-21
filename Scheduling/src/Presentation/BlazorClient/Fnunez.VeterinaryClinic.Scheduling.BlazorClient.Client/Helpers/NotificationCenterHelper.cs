using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.NotificationCenter;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;

public static class NotificationCenterHelper
{
    public static NotificationCenterItemVm MapNotificationCenterItemViewModel(
        AppNotificationListItem appNotificationListItem)
    {
        return new NotificationCenterItemVm
        {
            CreatedOn = appNotificationListItem.CreatedOn,
            Id = appNotificationListItem.Id,
            IsRead = appNotificationListItem.IsRead,
            Message = appNotificationListItem.Message,
            ModuleIcon = appNotificationListItem.ModuleIcon,
            Title = appNotificationListItem.Title,
            Url = appNotificationListItem.Url
        };
    }
}