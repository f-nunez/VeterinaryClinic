using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Notifications.Commands.DeleteAppNotifications;

public class DeleteAppNotificationsResponse : BaseResponse
{
    public DeleteAppNotificationsResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}