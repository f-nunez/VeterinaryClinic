using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.DeleteAllAppNotifications;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.DeleteAppNotification;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotifications;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotificationsDataGrid;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetUnreadAppNotificationsCount;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.MarkAppNotificationAsRead;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public class AppNotificationService : IAppNotificationService
{
    private readonly IClinicManagementNotificationsApiHttpService _httpService;
    private readonly ILogger<AppNotificationService> _logger;

    public AppNotificationService(
        IClinicManagementNotificationsApiHttpService httpService,
        ILogger<AppNotificationService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    public async Task DeleteAllAppNotificationsAsync(
        DeleteAllAppNotificationsRequest request)
    {
        _logger.LogInformation($"DeleteAllAppNotifications: {request}");

        await _httpService.HttpDeleteAsync<DeleteAllAppNotificationsRequest>(
            "Notification/DeleteAllAppNotifications"
        );
    }

    public async Task DeleteAppNotificationAsync(
        DeleteAppNotificationRequest request)
    {
        _logger.LogInformation($"DeleteAppNotification: {request}");

        await _httpService.HttpDeleteAsync<DeleteAppNotificationRequest>(
            "Notification/DeleteAppNotification", request.AppNotificationId
        );
    }

    public async Task<GetAppNotificationsResponse> GetAppNotificationsAsync(
        GetAppNotificationsRequest request)
    {
        _logger.LogInformation($"GetAppNotifications: {request}");

        var response = await _httpService
            .HttpPostAsync<GetAppNotificationsResponse>(
                "Notification/GetAppNotifications",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response;
    }

    public async Task<DataGridResponse<AppNotificationDto>> GetAppNotificationsDataGridAsync(
        GetAppNotificationsDataGridRequest request)
    {
        _logger.LogInformation($"GetAppNotificationsDataGrid: {request}");

        var response = await _httpService
            .HttpPostAsync<GetAppNotificationsDataGridResponse>(
                "Notification/GetAppNotificationsDataGrid",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (response.DataGridResponse is null)
            throw new ArgumentNullException(nameof(response.DataGridResponse));

        return response.DataGridResponse;
    }

    public async Task<int> GetUnreadAppNotificationsCountAsync(
        GetUnreadAppNotificationsCountRequest request)
    {
        _logger.LogInformation($"GetUnreadAppNotificationsCount: {request}");

        var response = await _httpService
            .HttpPostAsync<GetUnreadAppNotificationsCountResponse>(
                "Notification/GetUnreadAppNotificationsCount",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Count;
    }

    public async Task MarkAppNotificationAsReadAsync(
        MarkAppNotificationAsReadRequest request)
    {
        _logger.LogInformation($"MarkAppNotificationAsRead: {request}");

        var response = await _httpService
            .HttpPutAsync<MarkAppNotificationAsReadRequest>(
                "Notification/MarkAppNotificationAsRead",
                request
            );
    }
}