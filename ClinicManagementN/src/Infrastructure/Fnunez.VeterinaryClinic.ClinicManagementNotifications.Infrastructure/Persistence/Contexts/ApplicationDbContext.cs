using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Infrastructure.Persistence.Contexts;

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