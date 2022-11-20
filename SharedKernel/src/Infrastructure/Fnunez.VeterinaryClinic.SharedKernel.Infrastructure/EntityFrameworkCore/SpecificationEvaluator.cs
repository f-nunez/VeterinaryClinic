using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore;

public class SpecificationEvaluator<T, TResult> : SpecificationEvaluator<T> where T : class
{
    public static IQueryable<TResult> GetQuery(IQueryable<T> query, ISpecification<T, TResult> specification)
    {
        if (specification.Selector is null)
            throw new ArgumentNullException(nameof(specification.Selector));

        query = SpecificationEvaluator<T>.GetQuery(query, specification as ISpecification<T>);

        return query.Select(specification.Selector);
    }
}

public class SpecificationEvaluator<T> where T : class
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> specification)
    {
        if (specification.AsNoTracking)
            query = query.AsNoTracking();

        if (specification.Criteria != null)
            query = query.Where(specification.Criteria);

        if (specification.OrderBy != null)
            query = query.OrderBy(specification.OrderBy);

        if (specification.OrderByDescending != null)
            query = query.OrderByDescending(specification.OrderByDescending);

        if (specification.IsPagingEnabled)
            query = query.Skip(specification.Skip).Take(specification.Take);

        if (specification.Includes.Any())
            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}