using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore;

public class ReadRepository<T>
    : BaseReadRepository<T> where T : class, IAggregateRoot
{
    public ReadRepository(DbContext dbContext) : base(dbContext)
    {
    }
}