using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.GetAppNotifications;

public class GetAppNotificationsResponse : BaseResponse
{
    public List<AppNotificationDto> AppNotifications { get; set; } = new();
    public int Count { get; set; }

    public GetAppNotificationsResponse(Guid correlationId) : base(correlationId)
    {
    }
}