using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task<int> CommitAsync(CancellationToken cancellationToken);
    IReadRepository<T> ReadRepository<T>() where T : class, IAggregateRoot;
    IRepository<T> Repository<T>() where T : class, IAggregateRoot;
    Task RollbackAsync();
}