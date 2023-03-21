using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries.GetAppNotifications;

public class AppNotificationSpecification : BaseSpecification<AppNotification>
{
    public AppNotificationSpecification(
        GetAppNotificationsRequest request,
        string? userId)
    {
        Query
            .AsNoTracking()
            .Include(an => an.Notification)
            .ThenInclude(n => n.TriggeredByUser)
            .OrderByDescending(an => an.CreatedOn)
            .Skip(request.Skip)
            .Take(request.Take)
            .Where(an => an.UserId == userId && an.IsActive);
    }
}