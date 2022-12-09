using System.Linq.Expressions;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Expressions;

namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

public static class IncludableBuilderExtensions
{
    /// <summary>
    /// Specify an include expression.
    /// This information is utilized to build ThenInclude function in the query, which ORM tools like Entity Framework use
    /// to include related chained entities (via navigation properties) in the query result.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="previousBuilder"></param>
    /// <param name="thenIncludeExpression"></param>
    public static IIncludableSpecificationBuilder<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
        this IIncludableSpecificationBuilder<TEntity, TPreviousProperty> previousBuilder,
        Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression)
        where TEntity : class
    {
        var expression = new IncludeExpression(
            thenIncludeExpression,
            typeof(TEntity),
            typeof(TProperty),
            typeof(TPreviousProperty)
        );

        previousBuilder.Specification.IncludeExpressions.Add(expression);

        return new IncludableSpecificationBuilder<TEntity, TProperty>(
            previousBuilder.Specification);
    }
}