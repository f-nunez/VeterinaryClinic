using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;
using Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore.Specifications.Extensions;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore.Specifications.Evaluators;

public static class SearchExpressionEvaluator
{
    public static IQueryable<T> ComputeSearchExpressions<T>(
        IQueryable<T> query,
        ISpecification<T> specification) where T : class
    {
        var groupedSearchCriterias = specification.SearchExpressions
            .GroupBy(x => x.SearchGroup);

        foreach (var searchCriteria in groupedSearchCriterias)
            query = query.Search(searchCriteria);

        return query;
    }
}