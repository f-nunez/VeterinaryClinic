using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries.GetAppNotificationsDataGrid;

public class GetAppNotificationsDataGridResponse : BaseResponse
{
    public DataGridResponse<AppNotificationDto>? DataGridResponse { get; set; }

    public GetAppNotificationsDataGridResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}