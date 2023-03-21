using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.DeleteAppNotification;

public class DeleteAppNotificationResponse : BaseResponse
{
    public DeleteAppNotificationResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}