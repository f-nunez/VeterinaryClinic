using System.Linq.Expressions;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Expressions;

namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

public class BaseSpecification<T, TResult>
    : BaseSpecification<T>, ISpecification<T, TResult>
{
    public new virtual ISpecificationBuilder<T, TResult> Query { get; }
    /// <inheritdoc/>
    public Expression<Func<T, TResult>>? Selector { get; internal set; }

    protected BaseSpecification()
    {
        Query = new SpecificationBuilder<T, TResult>(this);
    }
}

public class BaseSpecification<T> : ISpecification<T>
{
    public bool AsNoTracking { get; internal set; }
    public IList<IncludeExpression> IncludeExpressions { get; internal set; }
    public IList<OrderExpression<T>> OrderExpressions { get; internal set; }
    public virtual ISpecificationBuilder<T> Query { get; }
    public IList<SearchExpression<T>> SearchExpressions { get; internal set; }
    public int? Skip { get; internal set; }
    public int? Take { get; internal set; }
    public IList<WhereExpression<T>> WhereExpressions { get; internal set; }


    protected BaseSpecification()
    {
        IncludeExpressions = new List<IncludeExpression>();
        OrderExpressions = new List<OrderExpression<T>>();
        Query = new SpecificationBuilder<T>(this);
        SearchExpressions = new List<SearchExpression<T>>();
        WhereExpressions = new List<WhereExpression<T>>();
    }
}