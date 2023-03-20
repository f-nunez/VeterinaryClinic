using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Commands.DeleteAppNotifications;

public class DeleteAppNotificationsResponse : BaseResponse
{
    public DeleteAppNotificationsResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}