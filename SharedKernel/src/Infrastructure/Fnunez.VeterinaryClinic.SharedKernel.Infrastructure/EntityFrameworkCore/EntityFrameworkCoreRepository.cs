using System.Reflection;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore;

public abstract class EntityFrameworkCoreRepository<T>
    : EntityFrameworkCoreReadRepository<T>, IBaseRepository<T> where T : class
{
    private const string IsActive = "IsActive";

    public EntityFrameworkCoreRepository(DbContext dbContext)
        : base(dbContext)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<T> AddAsync(
        T entity,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<T>> AddRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddRangeAsync(entities);
        return entities;
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(
        T entity,
        CancellationToken cancellationToken = default)
    {
        TrySetProperty(entity, IsActive, false);

        _dbContext.Set<T>().Update(entity);
        await Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual async Task DeleteRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
            TrySetProperty(entity, IsActive, false);

        _dbContext.Set<T>().UpdateRange(entities);
        await Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual async Task HardDeleteAsync(
        T entity,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Remove(entity);
        await Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual async Task HardDeleteRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().RemoveRange(entities);
        await Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync(
        T entity,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Update(entity);
        await Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual async Task UpdateRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().UpdateRange(entities);
        await Task.CompletedTask;
    }

    private void TrySetProperty(object obj, string property, object value)
    {
        var foundProperty = obj
            .GetType()
            .GetProperty(property, BindingFlags.Public | BindingFlags.Instance);

        if (foundProperty != null && foundProperty.CanWrite)
            foundProperty.SetValue(obj, value, null);
    }
}