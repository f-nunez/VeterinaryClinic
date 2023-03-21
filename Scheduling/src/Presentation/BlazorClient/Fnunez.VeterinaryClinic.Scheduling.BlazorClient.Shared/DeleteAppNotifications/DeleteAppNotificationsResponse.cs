using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Notifications.Commands.DeleteAppNotifications;

public class DeleteAppNotificationsResponse : BaseResponse
{
    public DeleteAppNotificationsResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}