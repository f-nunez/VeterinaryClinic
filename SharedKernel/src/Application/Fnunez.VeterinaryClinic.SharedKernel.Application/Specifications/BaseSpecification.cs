using System.Linq.Expressions;

namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

public class BaseSpecification<T, TResult> : BaseSpecification<T>, ISpecification<T, TResult>
{
    /// <inheritdoc/>
    public Expression<Func<T, TResult>>? Selector { get; set; }
}

public class BaseSpecification<T> : ISpecification<T>
{
    public bool AsNoTracking { get; private set; }
    public Expression<Func<T, bool>>? Criteria { get; private set; }
    public List<Expression<Func<T, object>>> Includes { get; private set; }
    public bool IsPagingEnabled { get; private set; }
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public int Skip { get; private set; }
    public int Take { get; private set; }

    public BaseSpecification()
    {
        Includes = new List<Expression<Func<T, object>>>();
    }

    protected void AddAsNoTracking()
    {
        AsNoTracking = true;
    }

    protected void AddCriteria(Expression<Func<T, bool>> expression)
    {
        Criteria = expression;
    }

    protected void AddInclude(Expression<Func<T, object>> expression)
    {
        Includes.Add(expression);
    }

    protected void AddOrderBy(Expression<Func<T, object>> expression)
    {
        OrderBy = expression;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> expression)
    {
        OrderByDescending = expression;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}