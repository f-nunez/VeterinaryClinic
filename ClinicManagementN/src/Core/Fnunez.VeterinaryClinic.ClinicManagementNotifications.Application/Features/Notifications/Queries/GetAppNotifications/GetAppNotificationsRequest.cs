using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries.GetAppNotifications;

public class GetAppNotificationsRequest : BaseRequest
{
    public int Skip { get; set; }
    public int Take { get; set; }
}