using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.MarkAppNotificationAsRead;

public class MarkAppNotificationAsReadResponse : BaseResponse
{
    public MarkAppNotificationAsReadResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}