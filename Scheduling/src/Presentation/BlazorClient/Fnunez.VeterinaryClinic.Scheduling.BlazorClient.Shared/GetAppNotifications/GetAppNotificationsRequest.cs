using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.GetAppNotifications;

public class GetAppNotificationsRequest : BaseRequest
{
    public int Skip { get; set; }
    public int Take { get; set; }
}