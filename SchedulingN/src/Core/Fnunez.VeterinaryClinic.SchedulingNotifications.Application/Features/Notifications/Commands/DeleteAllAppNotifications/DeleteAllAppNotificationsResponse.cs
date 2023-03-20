using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.DeleteAllAppNotifications;

public class DeleteAllAppNotificationsResponse : BaseResponse
{
    public DeleteAllAppNotificationsResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}