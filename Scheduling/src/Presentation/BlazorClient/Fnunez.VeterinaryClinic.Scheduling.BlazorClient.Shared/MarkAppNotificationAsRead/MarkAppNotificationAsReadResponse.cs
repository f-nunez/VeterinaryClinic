using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.MarkAppNotificationAsRead;

public class MarkAppNotificationAsReadResponse : BaseResponse
{
    public MarkAppNotificationAsReadResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}