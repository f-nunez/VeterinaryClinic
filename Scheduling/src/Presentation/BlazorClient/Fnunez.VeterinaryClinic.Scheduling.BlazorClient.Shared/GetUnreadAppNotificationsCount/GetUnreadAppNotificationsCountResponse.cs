using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.GetUnreadAppNotificationsCount;

public class GetUnreadAppNotificationsCountResponse : BaseResponse
{
    public int Count { get; set; }

    public GetUnreadAppNotificationsCountResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}