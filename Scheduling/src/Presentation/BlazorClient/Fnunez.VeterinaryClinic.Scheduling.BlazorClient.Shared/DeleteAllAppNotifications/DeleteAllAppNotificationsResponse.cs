using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.DeleteAllAppNotifications;

public class DeleteAllAppNotificationsResponse : BaseResponse
{
    public DeleteAllAppNotificationsResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}