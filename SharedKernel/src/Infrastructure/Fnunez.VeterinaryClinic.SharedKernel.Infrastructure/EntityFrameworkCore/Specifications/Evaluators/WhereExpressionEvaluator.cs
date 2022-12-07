using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore.Specifications.Evaluators;

public static class WhereExpressionEvaluator
{
    public static IQueryable<R> ComputeWhereExpressions<R>(
        IQueryable<R> query,
        ISpecification<R> specification) where R : class
    {
        foreach (var expression in specification.WhereExpressions)
            query = query.Where(expression.Filter);

        return query;
    }
}