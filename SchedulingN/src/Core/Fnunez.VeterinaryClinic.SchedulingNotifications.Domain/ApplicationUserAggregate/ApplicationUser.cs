using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationUserRoleAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationUserAggregate;

public class ApplicationUser : BaseEntity<string>, IAggregateRoot
{
    public string? Name { get; private set; }

    public IReadOnlyList<ApplicationUserRole> UserRoles { get; set; } = null!;

    public ApplicationUser()
    {
    }

    public ApplicationUser(string id, string name)
    {
        if (string.IsNullOrEmpty(id))
            throw new ArgumentException(
                $"Required input {nameof(id)} was empty.",
                nameof(id));

        if (string.IsNullOrEmpty(name))
            throw new ArgumentException(
                $"Required input {nameof(name)} was empty.",
                nameof(name));

        Id = id;
        Name = name;
    }
}