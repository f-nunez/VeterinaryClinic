using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.DeleteAllAppNotifications;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.DeleteAppNotification;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotifications;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetUnreadAppNotificationsCount;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.MarkAppNotificationAsRead;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public interface IAppNotificationService
{
    Task DeleteAllAppNotificationsAsync(DeleteAllAppNotificationsRequest request);
    Task DeleteAppNotificationAsync(DeleteAppNotificationRequest request);
    Task<GetAppNotificationsResponse> GetAppNotificationsAsync(GetAppNotificationsRequest request);
    Task<int> GetUnreadAppNotificationsCountAsync(GetUnreadAppNotificationsCountRequest request);
    Task MarkAppNotificationAsReadAsync(MarkAppNotificationAsReadRequest request);
}