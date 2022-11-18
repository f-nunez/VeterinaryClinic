using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;

public interface IRepository<T> : IBaseRepository<T>, IBaseReadRepository<T> where T : class, IAggregateRoot
{
}