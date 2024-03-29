using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore;

public abstract class BaseUnitOfWork : IUnitOfWork
{
    private bool _disposed;
    private readonly DbContext _dbContext;
    private Dictionary<Type, object> _readRepositories;
    private Dictionary<Type, object> _repositories;

    public BaseUnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
        _readRepositories = new Dictionary<Type, object>();
        _repositories = new Dictionary<Type, object>();
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public virtual async Task<int> ExecuteSqlCommandAsync(
        string sql,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Database.ExecuteSqlRawAsync(
            sql, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<int> ExecuteSqlCommandAsync(
        string sql,
        IEnumerable<object> parameters,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Database.ExecuteSqlRawAsync(
            sql, parameters, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> GetFromRawSqlAsync<T>(
        string sql,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(sql))
            throw new ArgumentNullException(nameof(sql));

        return await _dbContext.GetFromQueryAsync<T>(
            sql, new List<object>(), cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> GetFromRawSqlAsync<T>(
        string sql,
        object parameter,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(sql))
            throw new ArgumentNullException(nameof(sql));

        return await _dbContext.GetFromQueryAsync<T>(
            sql, new List<object>() { parameter }, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> GetFromRawSqlAsync<T>(
        string sql,
        IEnumerable<object> parameters,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(sql))
            throw new ArgumentNullException(nameof(sql));

        return await _dbContext.GetFromQueryAsync<T>(
            sql, parameters, cancellationToken);
    }

    /// <inheritdoc/>
    IReadRepository<T> IUnitOfWork.ReadRepository<T>()
    {
        Type entityType = typeof(T);
        if (!_readRepositories.ContainsKey(entityType))
            _readRepositories[entityType] = new ReadRepository<T>(_dbContext);

        return (IReadRepository<T>)_readRepositories[entityType];
    }

    /// <inheritdoc/>
    IRepository<T> IUnitOfWork.Repository<T>()
    {
        Type entityType = typeof(T);
        if (!_repositories.ContainsKey(entityType))
            _repositories[entityType] = new Repository<T>(_dbContext);

        return (IRepository<T>)_repositories[entityType];
    }

    /// <inheritdoc/>
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
            if (_readRepositories != null)
                _readRepositories.Clear();

            if (_repositories != null)
                _repositories.Clear();

            _dbContext.Dispose();
        }

        _disposed = true;
    }
}