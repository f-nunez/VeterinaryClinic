using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Commands.DeleteAppNotification;

public class AppNotificationSpecification : BaseSpecification<AppNotification>
{
    public AppNotificationSpecification(Guid appNotificationId, string? userId)
    {
        Query.Where(an => an.Id == appNotificationId && an.UserId == userId);
    }
}