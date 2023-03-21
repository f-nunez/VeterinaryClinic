using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.MarkAppNotificationAsRead;

public class MarkAppNotificationAsReadRequest : BaseRequest
{
    public Guid AppNotificationId { get; set; }
}