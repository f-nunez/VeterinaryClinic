using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries.GetUnreadAppNotificationsCount;

public class UnreadAppNotificationSpecification
    : BaseSpecification<AppNotification>
{
    public UnreadAppNotificationSpecification(string? userId)
    {
        Query
            .AsNoTracking()
            .Where(an =>
                an.UserId == userId &&
                an.IsActive &&
                an.ReadOn == null
            );
    }
}