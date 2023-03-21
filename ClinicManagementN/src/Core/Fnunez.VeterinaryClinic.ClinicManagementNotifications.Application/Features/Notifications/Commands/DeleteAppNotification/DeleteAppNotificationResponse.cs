using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Commands.DeleteAppNotification;

public class DeleteAppNotificationResponse : BaseResponse
{
    public DeleteAppNotificationResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}