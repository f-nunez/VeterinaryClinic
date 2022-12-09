using System.Linq.Expressions;

namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Expressions;

public class WhereExpression<T>
{
    /// <summary>
    /// Condition which should be satisfied by instances of <typeparamref name="T"/>.
    /// </summary>
    public Expression<Func<T, bool>> Filter { get; }

    /// <summary>
    /// Creates instance of <see cref="WhereExpression{T}" />.
    /// </summary>
    /// <param name="filter">Condition which should be satisfied by instances of <typeparamref name="T"/>.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="filter"/> is null.</exception>
    public WhereExpression(Expression<Func<T, bool>> filter)
    {
        if (filter is null)
            throw new ArgumentNullException(nameof(filter));

        Filter = filter;
    }
}