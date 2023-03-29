using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationUserAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Specifications;

public class UsersExceptWhoTriggerTheEventSpecification
    : BaseSpecification<ApplicationUser>
{
    private const string SchedulingAppId = "00000001-0000-0000-0000-000000000000";

    public UsersExceptWhoTriggerTheEventSpecification(string? triggeredByUserId)
    {
        Query
            .AsNoTracking()
            .Where(au =>
                au.IsActive &&
                au.Id != triggeredByUserId &&
                au.Id != SchedulingAppId);
    }
}