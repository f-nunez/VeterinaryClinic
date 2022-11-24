using System.Linq.Expressions;

namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

public interface ISpecification<T, TResult> : ISpecification<T>
{
    /// <summary>
    /// The transform function to apply to the <typeparamref name="T"/> element.
    /// </summary>
    Expression<Func<T, TResult>>? Selector { get; set; }
}

public interface ISpecification<T>
{
    bool AsNoTracking { get; }
    Expression<Func<T, bool>>? Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    bool IsPagingEnabled { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    int Skip { get; }
    int Take { get; }
}