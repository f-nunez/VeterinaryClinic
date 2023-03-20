using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Notifications.Commands.DeleteAppNotifications;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.DeleteAllAppNotifications;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.DeleteAppNotification;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotifications;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotificationsDataGrid;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetUnreadAppNotificationsCount;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.MarkAppNotificationAsRead;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

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