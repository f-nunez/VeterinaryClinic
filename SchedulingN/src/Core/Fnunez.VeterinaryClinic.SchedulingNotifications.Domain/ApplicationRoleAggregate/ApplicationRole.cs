using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationRoleAggregate;

public class ApplicationRole : BaseEntity<string>, IAggregateRoot
{
    public string? Name { get; private set; }

    public ApplicationRole()
    {
    }

    public ApplicationRole(string id, string name)
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