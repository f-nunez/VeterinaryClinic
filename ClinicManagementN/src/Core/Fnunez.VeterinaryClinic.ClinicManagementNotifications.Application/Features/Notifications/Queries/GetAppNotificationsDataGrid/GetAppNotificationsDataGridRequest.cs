using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries.GetAppNotificationsDataGrid;

public class GetAppNotificationsDataGridRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
    public bool OnlyReadFilterValue { get; set; }
    public bool OnlyUnreadFilterValue { get; set; }
}