using System.Reflection;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationRoleAggregate;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationUserAggregate;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationUserRoleAggregate;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    private readonly IMediator _mediator;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator)
        : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<ApplicationRole> ApplicationRoles => Set<ApplicationRole>();
    public DbSet<ApplicationUserRole> ApplicationUserRoles => Set<ApplicationUserRole>();
    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    public DbSet<AppNotification> AppNotifications => Set<AppNotification>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        if (_mediator != null)
            await _mediator.DispatchDomainEventsAsync(this);

        return result;
    }
}