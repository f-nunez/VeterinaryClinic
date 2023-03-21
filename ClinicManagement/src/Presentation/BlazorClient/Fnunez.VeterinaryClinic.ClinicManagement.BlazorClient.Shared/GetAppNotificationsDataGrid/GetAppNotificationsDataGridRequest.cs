using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotificationsDataGrid;
public class GetAppNotificationsDataGridRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
    public bool OnlyReadFilterValue { get; set; }
    public bool OnlyUnreadFilterValue { get; set; }
}