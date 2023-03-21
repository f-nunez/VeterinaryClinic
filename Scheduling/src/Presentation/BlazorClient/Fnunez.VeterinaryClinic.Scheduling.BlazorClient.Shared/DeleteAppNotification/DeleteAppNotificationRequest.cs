using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Shared.DeleteAppNotification;

public class DeleteAppNotificationRequest : BaseRequest
{
    public Guid AppNotificationId { get; set; }
}