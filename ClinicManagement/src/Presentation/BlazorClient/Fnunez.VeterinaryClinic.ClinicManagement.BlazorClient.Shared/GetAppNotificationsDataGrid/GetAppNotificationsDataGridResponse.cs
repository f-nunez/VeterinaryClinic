using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotificationsDataGrid;

public class GetAppNotificationsDataGridResponse : BaseResponse
{
    public DataGridResponse<AppNotificationDto>? DataGridResponse { get; set; }

    public GetAppNotificationsDataGridResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}