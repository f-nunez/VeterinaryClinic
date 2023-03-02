using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries.GetAppNotifications;

public class GetAppNotificationsResponse : BaseResponse
{
    public List<AppNotificationDto> AppNotifications { get; set; } = new();

    public GetAppNotificationsResponse(Guid correlationId) : base(correlationId)
    {
    }
}