using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Queries.GetAppNotificationsDataGrid;

public class GetAppNotificationsDataGridResponse : BaseResponse
{
    public DataGridResponse<AppNotificationDto>? DataGridResponse { get; set; }

    public GetAppNotificationsDataGridResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}