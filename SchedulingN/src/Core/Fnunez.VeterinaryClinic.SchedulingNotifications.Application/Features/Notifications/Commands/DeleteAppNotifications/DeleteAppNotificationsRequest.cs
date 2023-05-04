using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.DeleteAppNotifications;

public class DeleteAppNotificationsRequest : BaseRequest
{
    public List<Guid> AppNotificationIds { get; set; } = new();
}