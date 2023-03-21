using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.ApplicationUserAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Specifications;

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