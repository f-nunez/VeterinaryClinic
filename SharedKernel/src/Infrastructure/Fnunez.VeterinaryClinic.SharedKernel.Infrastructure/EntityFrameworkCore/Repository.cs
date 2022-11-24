using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore;

public class Repository<T> : BaseRepository<T> where T : class, IAggregateRoot
{
    public Repository(DbContext dbContext) : base(dbContext)
    {
    }
}