using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Queries.GetUnreadAppNotificationsCount;

public class GetUnreadAppNotificationsCountResponse : BaseResponse
{
    public int Count { get; set; }

    public GetUnreadAppNotificationsCountResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}