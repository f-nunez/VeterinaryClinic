using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;
using Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore.Specifications.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore.Specifications.Evaluators;

public class SpecificationEvaluator<T, TResult>
    : SpecificationEvaluator<T> where T : class
{
    public static IQueryable<TResult> GetCountQuery(
        IQueryable<T> query,
        ISpecification<T, TResult> specification)
    {
        if (specification.Selector is null)
            throw new MissedSelectorException();

        query = SpecificationEvaluator<T>
            .GetCountQuery(query, specification as ISpecification<T>);

        return query.Select(specification.Selector);
    }

    public static IQueryable<TResult> GetQuery(
        IQueryable<T> query,
        ISpecification<T, TResult> specification)
    {
        if (specification.Selector is null)
            throw new MissedSelectorException();

        query = SpecificationEvaluator<T>
            .GetQuery(query, specification as ISpecification<T>);

        return query.Select(specification.Selector);
    }
}

public class SpecificationEvaluator<T> where T : class
{
    public static IQueryable<T> GetCountQuery(
        IQueryable<T> query,
        ISpecification<T> specification)
    {
        if (specification.AsNoTracking)
            query = query.AsNoTracking();

        if (specification.IncludeExpressions.Count > 0)
            query = IncludeExpressionEvaluator
                .ComputeIncludeExpressions(query, specification);

        if (specification.WhereExpressions.Count > 0)
            query = WhereExpressionEvaluator
                .ComputeWhereExpressions(query, specification);

        if (specification.SearchExpressions.Count > 0)
            query = SearchExpressionEvaluator
                .ComputeSearchExpressions(query, specification);

        return query;
    }

    public static IQueryable<T> GetQuery(
        IQueryable<T> query,
        ISpecification<T> specification)
    {
        if (specification.AsNoTracking)
            query = query.AsNoTracking();

        if (specification.IncludeExpressions.Count > 0)
            query = IncludeExpressionEvaluator
                .ComputeIncludeExpressions(query, specification);

        if (specification.WhereExpressions.Count > 0)
            query = WhereExpressionEvaluator
                .ComputeWhereExpressions(query, specification);

        if (specification.OrderExpressions.Count > 0)
            query = OrderExpressionEvaluator
                .ComputeOrderExpressions(query, specification);

        if (specification.SearchExpressions.Count > 0)
            query = SearchExpressionEvaluator
                .ComputeSearchExpressions(query, specification);

        if (specification.Skip != null && specification.Skip > 0)
            query = query.Skip(specification.Skip.Value);

        if (specification.Take != null && specification.Take > 0)
            query = query.Take(specification.Take.Value);

        return query;
    }
}