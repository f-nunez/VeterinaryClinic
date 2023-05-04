using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.DeleteAllAppNotifications;

public class DeleteAllAppNotificationsResponse : BaseResponse
{
    public DeleteAllAppNotificationsResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}