using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore;

public abstract class BaseReadRepository<T> : EntityFrameworkCoreReadRepository<T>, IReadRepository<T> where T : class, IAggregateRoot
{
    public BaseReadRepository(DbContext dbContext) : base(dbContext)
    {
    }
}