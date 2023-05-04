using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Notifications.Commands.DeleteAppNotifications;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.DeleteAllAppNotifications;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.DeleteAppNotification;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.GetAppNotifications;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.GetAppNotificationsDataGrid;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.GetUnreadAppNotificationsCount;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.MarkAppNotificationAsRead;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public interface IAppNotificationService
{
    Task DeleteAllAppNotificationsAsync(DeleteAllAppNotificationsRequest request);
    Task DeleteAppNotificationAsync(DeleteAppNotificationRequest request);
    Task DeleteAppNotificationsAsync(DeleteAppNotificationsRequest request);
    Task<GetAppNotificationsResponse> GetAppNotificationsAsync(GetAppNotificationsRequest request);
    Task<DataGridResponse<AppNotificationDto>> GetAppNotificationsDataGridAsync(GetAppNotificationsDataGridRequest request);
    Task<int> GetUnreadAppNotificationsCountAsync(GetUnreadAppNotificationsCountRequest request);
    Task MarkAppNotificationAsReadAsync(MarkAppNotificationAsReadRequest request);
}