using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;

public interface IReadRepository<T> : IBaseReadRepository<T> where T : class, IAggregateRoot
{
}