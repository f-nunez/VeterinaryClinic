using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.MarkAppNotificationAsRead;

public class MarkAppNotificationAsReadResponse : BaseResponse
{
    public MarkAppNotificationAsReadResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}