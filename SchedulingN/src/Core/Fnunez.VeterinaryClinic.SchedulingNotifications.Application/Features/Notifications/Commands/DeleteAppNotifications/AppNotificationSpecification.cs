using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.DeleteAppNotifications;

public class AppNotificationSpecification : BaseSpecification<AppNotification>
{
    public AppNotificationSpecification(
        DeleteAppNotificationsRequest request,
        string? userId)
    {
        Query
            .Where(an => request.AppNotificationIds.Contains(an.Id))
            .Where(an => an.UserId == userId)
            .Where(an => an.IsActive);
    }
}