using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Notifications.Commands.DeleteAppNotifications;

public class DeleteAppNotificationsRequest : BaseRequest
{
    public List<Guid> AppNotificationIds { get; set; } = new();
}