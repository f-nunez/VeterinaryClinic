using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.DeleteAppNotifications;

public class DeleteAppNotificationsResponse : BaseResponse
{
    public DeleteAppNotificationsResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}