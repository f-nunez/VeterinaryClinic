using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Commands.DeleteAllAppNotifications;

public class AppNotificationSpecification : BaseSpecification<AppNotification>
{
    public AppNotificationSpecification(string? userId)
    {

        Query.Where(an => an.UserId == userId && an.IsActive);
    }
}