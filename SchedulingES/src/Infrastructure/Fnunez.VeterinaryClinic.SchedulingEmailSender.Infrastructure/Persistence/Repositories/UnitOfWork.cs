using Fnunez.VeterinaryClinic.SchedulingEmailSender.Infrastructure.Persistence.Contexts;
using Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Infrastructure.Persistence.Repositories;

public class UnitOfWork : BaseUnitOfWork
{
    public UnitOfWork(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}