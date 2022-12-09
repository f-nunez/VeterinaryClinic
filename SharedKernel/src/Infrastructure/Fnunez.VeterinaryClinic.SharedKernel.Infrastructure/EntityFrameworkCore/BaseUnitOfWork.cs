using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore;

public abstract class BaseUnitOfWork : IUnitOfWork
{
    private bool _disposed;
    private readonly DbContext _dbContext;
    private Dictionary<Type, object> _repositories;

    public BaseUnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new Dictionary<Type, object>();
    }

    public async Task<int> CommitAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    IReadRepository<T> IUnitOfWork.ReadRepository<T>()
    {
        Type entityType = typeof(T);
        if (!_repositories.ContainsKey(entityType))
            _repositories[entityType] = new ReadRepository<T>(_dbContext);

        return (IReadRepository<T>)_repositories[entityType];
    }

    IRepository<T> IUnitOfWork.Repository<T>()
    {
        Type entityType = typeof(T);
        if (!_repositories.ContainsKey(entityType))
            _repositories[entityType] = new Repository<T>(_dbContext);

        return (IRepository<T>)_repositories[entityType];
    }

    public async Task RollbackAsync()
    {
        _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        await Task.CompletedTask;
    }

    /// <summary>
    /// Performs application-defined tasks related to freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">Set disposing value.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            if (_repositories != null)
                _repositories.Clear();

            _dbContext.Dispose();
        }

        _disposed = true;
    }
}