using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationUserAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Specifications;

public class UsersExceptWhoTriggerTheEventSpecification
    : BaseSpecification<ApplicationUser>
{
    public UsersExceptWhoTriggerTheEventSpecification(string? triggeredByUserId)
    {
        Query
            .AsNoTracking()
            .Where(au => au.IsActive && au.Id != triggeredByUserId);
    }
}