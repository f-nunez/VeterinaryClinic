using System.Reflection;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Infrastructure.Persistence.Contexts;

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

    public DbSet<Email> Emails => Set<Email>();

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