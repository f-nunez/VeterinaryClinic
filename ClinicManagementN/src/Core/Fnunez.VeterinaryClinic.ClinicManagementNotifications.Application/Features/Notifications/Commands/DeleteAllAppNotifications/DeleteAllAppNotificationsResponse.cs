using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Commands.DeleteAllAppNotifications;

public class DeleteAllAppNotificationsResponse : BaseResponse
{
    public DeleteAllAppNotificationsResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}