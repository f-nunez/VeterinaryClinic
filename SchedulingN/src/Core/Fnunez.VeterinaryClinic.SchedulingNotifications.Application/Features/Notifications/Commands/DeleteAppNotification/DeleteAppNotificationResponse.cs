using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.DeleteAppNotification;

public class DeleteAppNotificationResponse : BaseResponse
{
    public DeleteAppNotificationResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}