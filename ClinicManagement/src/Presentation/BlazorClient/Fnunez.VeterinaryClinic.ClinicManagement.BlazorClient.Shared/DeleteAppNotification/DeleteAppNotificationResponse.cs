using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.DeleteAppNotification;

public class DeleteAppNotificationResponse : BaseResponse
{
    public DeleteAppNotificationResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}