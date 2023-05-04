using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.MarkAppNotificationAsRead;

public class MarkAppNotificationAsReadRequest : BaseRequest
{
    public Guid AppNotificationId { get; set; }
}