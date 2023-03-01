using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.ApplicationRoleAggregate;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.ApplicationUserAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.ApplicationUserRoleAggregate;

public class ApplicationUserRole : BaseEntity<Guid>, IAggregateRoot
{
    public string? RoleId { get; set; }
    public string? UserId { get; set; }

    #region Navigations
    public ApplicationRole Role { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
    #endregion

    public ApplicationUserRole()
    {
    }

    public ApplicationUserRole(Guid id, string roleId, string userId)
    {
        if (id == Guid.Empty)
            throw new ArgumentException(
                $"Required input {nameof(id)} was empty.",
                nameof(id));

        if (string.IsNullOrEmpty(roleId))
            throw new ArgumentException(
                $"Required input {nameof(roleId)} was empty.",
                nameof(roleId));

        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException(
                $"Required input {nameof(userId)} was empty.",
                nameof(userId));

        Id = id;
        RoleId = roleId;
        UserId = userId;
    }
}