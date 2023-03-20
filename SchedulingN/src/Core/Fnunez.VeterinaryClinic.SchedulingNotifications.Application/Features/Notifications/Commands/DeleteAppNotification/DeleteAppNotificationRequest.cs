using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.DeleteAppNotification;

public class DeleteAppNotificationRequest : BaseRequest
{
    public Guid AppNotificationId { get; set; }
}