using System.Linq.Expressions;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Expressions;

namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

public interface ISpecification<T, TResult> : ISpecification<T>
{
    new ISpecificationBuilder<T, TResult> Query { get; }
    /// <summary>
    /// The transform function to apply to the <typeparamref name="T"/> element.
    /// </summary>
    Expression<Func<T, TResult>>? Selector { get; }
}

public interface ISpecification<T>
{
    bool AsNoTracking { get; }
    IList<IncludeExpression> IncludeExpressions { get; }
    IList<OrderExpression<T>> OrderExpressions { get; }
    ISpecificationBuilder<T> Query { get; }
    IList<SearchExpression<T>> SearchExpressions { get; }
    int? Skip { get; }
    int? Take { get; }
    IList<WhereExpression<T>> WhereExpressions { get; }
}