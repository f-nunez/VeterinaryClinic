using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetUnreadAppNotificationsCount;

public class GetUnreadAppNotificationsCountResponse : BaseResponse
{
    public int Count { get; set; }

    public GetUnreadAppNotificationsCountResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}