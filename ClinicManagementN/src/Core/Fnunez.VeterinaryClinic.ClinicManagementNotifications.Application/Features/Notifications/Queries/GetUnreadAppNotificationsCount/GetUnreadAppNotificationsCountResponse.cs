using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries.GetUnreadAppNotificationsCount;

public class GetUnreadAppNotificationsCountResponse : BaseResponse
{
    public int Count { get; set; }

    public GetUnreadAppNotificationsCountResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}