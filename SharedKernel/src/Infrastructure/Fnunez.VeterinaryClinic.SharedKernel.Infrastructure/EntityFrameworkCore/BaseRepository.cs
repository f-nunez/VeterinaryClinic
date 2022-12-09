using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore;

public abstract class BaseRepository<T>
    : EntityFrameworkCoreRepository<T>, IRepository<T> where T
    : class, IAggregateRoot
{
    public BaseRepository(DbContext dbContext) : base(dbContext)
    {
    }
}