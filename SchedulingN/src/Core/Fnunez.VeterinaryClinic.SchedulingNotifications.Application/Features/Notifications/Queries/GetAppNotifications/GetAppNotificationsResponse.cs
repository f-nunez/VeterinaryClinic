using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Queries.GetAppNotifications;

public class GetAppNotificationsResponse : BaseResponse
{
    public List<AppNotificationDto> AppNotifications { get; set; } = new();
    public int Count { get; set; }

    public GetAppNotificationsResponse(Guid correlationId) : base(correlationId)
    {
    }
}