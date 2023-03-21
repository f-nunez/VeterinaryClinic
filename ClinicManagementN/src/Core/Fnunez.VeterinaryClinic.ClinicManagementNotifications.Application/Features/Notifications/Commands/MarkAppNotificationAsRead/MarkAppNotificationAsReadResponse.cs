using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Commands.MarkAppNotificationAsRead;

public class MarkAppNotificationAsReadResponse : BaseResponse
{
    public MarkAppNotificationAsReadResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}