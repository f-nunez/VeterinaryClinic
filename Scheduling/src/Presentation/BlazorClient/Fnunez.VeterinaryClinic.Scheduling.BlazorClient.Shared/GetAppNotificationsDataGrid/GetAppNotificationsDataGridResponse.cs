using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.GetAppNotificationsDataGrid;

public class GetAppNotificationsDataGridResponse : BaseResponse
{
    public DataGridResponse<AppNotificationDto>? DataGridResponse { get; set; }

    public GetAppNotificationsDataGridResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}