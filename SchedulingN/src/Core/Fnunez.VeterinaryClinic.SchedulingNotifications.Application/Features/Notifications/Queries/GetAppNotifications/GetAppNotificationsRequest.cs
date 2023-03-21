using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Queries.GetAppNotifications;

public class GetAppNotificationsRequest : BaseRequest
{
    public int Skip { get; set; }
    public int Take { get; set; }
}