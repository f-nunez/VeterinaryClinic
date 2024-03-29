using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Commit changes in the database.
    /// </summary>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while
    /// waiting for the task to complete.</param>
    /// <returns>The number of state entities written to database.</returns>
    Task<int> CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the specified raw SQL command in the database.
    /// </summary>
    /// <param name="sql">The raw SQL.</param>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while
    /// waiting for the task to complete.</param>
    /// <returns>The number of state entities written to database.</returns>
    Task<int> ExecuteSqlCommandAsync(
        string sql,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the specified raw SQL command in the database.
    /// </summary>
    /// <param name="sql">The raw SQL.</param>
    /// <param name="parameters">The parameters in the raw SQL.</param>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while
    /// waiting for the task to complete.</param>
    /// <returns>The number of state entities written to database.</returns>
    Task<int> ExecuteSqlCommandAsync(
        string sql,
        IEnumerable<object> parameters,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// This method takes <paramref name="sql"/> string as parameter and returns the result of the provided sql.
    /// </summary>
    /// <typeparam name="T">The <see langword="type"/> to which the result will be mapped.</typeparam>
    /// <param name="sql">The sql query string.</param>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while
    /// waiting for the task to complete.</param>
    /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="sql"/> is <see langword="null"/>.</exception>
    Task<List<T>> GetFromRawSqlAsync<T>(
        string sql,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// This method takes <paramref name="sql"/> string and the value of <paramref name="parameter"/> mentioned in the sql query as parameters
    /// and returns the result of the provided sql.
    /// </summary>
    /// <typeparam name="T">The <see langword="type"/> to which the result will be mapped.</typeparam>
    /// <param name="sql">The sql query string.</param>
    /// <param name="parameter">The value of the paramter mention in the sql query.</param>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while
    /// waiting for the task to complete.</param>
    /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="sql"/> is <see langword="null"/>.</exception>
    Task<List<T>> GetFromRawSqlAsync<T>(
        string sql,
        object parameter,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// This method takes <paramref name="sql"/> string and values of the <paramref name="parameters"/> mentioned in the sql query as parameters
    /// and returns the result of the provided sql.
    /// <para>
    /// The paramters names mentioned in the query should be like p0, p1,p2 etc.
    /// </para>
    /// </summary>
    /// <typeparam name="T">The <see langword="type"/> to which the result will be mapped.</typeparam>
    /// <param name="sql">The sql query string.</param>
    /// <param name="parameters">The values of the parameters mentioned in the sql query. The values should be primitive types.</param>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while
    /// waiting for the task to complete.</param>
    /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="sql"/> is <see langword="null"/>.</exception>
    Task<List<T>> GetFromRawSqlAsync<T>(string sql,
        IEnumerable<object> parameters,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtain read repository by entity type.
    /// </summary>
    IReadRepository<T> ReadRepository<T>() where T : class, IAggregateRoot;

    /// <summary>
    /// Obtain repository by entity type.
    /// </summary>
    IRepository<T> Repository<T>() where T : class, IAggregateRoot;

    /// <summary>
    /// Rollback changes in the database.
    /// </summary>
    Task RollbackAsync();
}