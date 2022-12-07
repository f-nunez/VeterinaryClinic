using System.Linq.Expressions;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Enums;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Expressions;

namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

public static class SpecificationBuilderExtensions
{
    /// <summary>
    /// If the entity instances are modified, this will not be detected
    /// by the change tracker.
    /// </summary>
    /// <param name="specificationBuilder"></param>
    public static ISpecificationBuilder<T> AsNoTracking<T>(
        this ISpecificationBuilder<T> specificationBuilder) where T : class
    {
        specificationBuilder.Specification.AsNoTracking = true;

        return specificationBuilder;
    }

    /// <summary>
    /// Specify an include expression.
    /// This information is utilized to build Include function in the query, which ORM tools like Entity Framework use
    /// to include related entities (via navigation properties) in the query result.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="specificationBuilder"></param>
    /// <param name="includeExpression"></param>
    public static IIncludableSpecificationBuilder<T, TProperty> Include<T, TProperty>(
        this ISpecificationBuilder<T> specificationBuilder,
        Expression<Func<T, TProperty>> includeExpression) where T : class
    {
        var expression = new IncludeExpression(
            includeExpression,
            typeof(T),
            typeof(TProperty)
        );

        specificationBuilder.Specification.IncludeExpressions.Add(expression);

        return new IncludableSpecificationBuilder<T, TProperty>(
            specificationBuilder.Specification);
    }

    /// <summary>
    /// Specify the query result will be ordered by <paramref name="orderExpression"/> in an ascending order
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="specificationBuilder"></param>
    /// <param name="orderExpression"></param>
    public static IOrderedSpecificationBuilder<T> OrderBy<T>(
        this ISpecificationBuilder<T> specificationBuilder,
        Expression<Func<T, object?>> orderExpression)
    {
        var expression = new OrderExpression<T>(
            orderExpression,
            OrderExpressionType.OrderBy
        );

        specificationBuilder.Specification.OrderExpressions.Add(expression);

        return new OrderedSpecificationBuilder<T>(
            specificationBuilder.Specification);
    }

    /// <summary>
    /// Specify the query result will be ordered by <paramref name="orderExpression"/> in a descending order
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="specificationBuilder"></param>
    /// <param name="orderExpression"></param>
    public static IOrderedSpecificationBuilder<T> OrderByDescending<T>(
        this ISpecificationBuilder<T> specificationBuilder,
        Expression<Func<T, object?>> orderExpression)
    {
        var expression = new OrderExpression<T>(
            orderExpression,
            OrderExpressionType.OrderByDescending
        );

        specificationBuilder.Specification.OrderExpressions.Add(expression);

        return new OrderedSpecificationBuilder<T>(
            specificationBuilder.Specification);
    }

    /// <summary>
    /// Specify a 'SQL LIKE' operations for search purposes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="specificationBuilder"></param>
    /// <param name="selector">the property to apply the SQL LIKE against</param>
    /// <param name="searchTerm">the value to use for the SQL LIKE</param>
    /// <param name="searchGroup">the index used to group sets of Selectors and SearchTerms together</param>
    public static ISpecificationBuilder<T> Search<T>(
        this ISpecificationBuilder<T> specificationBuilder,
        Expression<Func<T, string>> selector,
        string searchTerm,
        int searchGroup = 1) where T : class
    {
        var expression = new SearchExpression<T>(
            selector,
            searchTerm,
            searchGroup
        );

        specificationBuilder.Specification.SearchExpressions.Add(expression);

        return specificationBuilder;
    }

    /// <summary>
    /// Specify a transform function to apply to the <typeparamref name="T"/> element 
    /// to produce another <typeparamref name="TResult"/> element.
    /// </summary>
    public static ISpecificationBuilder<T, TResult> Select<T, TResult>(
        this ISpecificationBuilder<T, TResult> specificationBuilder,
        Expression<Func<T, TResult>> selector)
    {
        specificationBuilder.Specification.Selector = selector;

        return specificationBuilder;
    }

    /// <summary>
    /// Specify the number of elements to skip before returning the remaining elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="specificationBuilder"></param>
    /// <param name="skip">number of elements to skip</param>
    public static ISpecificationBuilder<T> Skip<T>(
        this ISpecificationBuilder<T> specificationBuilder,
        int skip)
    {
        specificationBuilder.Specification.Skip = skip;

        return specificationBuilder;
    }

    /// <summary>
    /// Specify the number of elements to return.
    /// </summary>
    /// <param name="specificationBuilder"></param>
    /// <param name="take">number of elements to take</param>
    public static ISpecificationBuilder<T> Take<T>(
        this ISpecificationBuilder<T> specificationBuilder,
        int take)
    {
        specificationBuilder.Specification.Take = take;

        return specificationBuilder;
    }

    /// <summary>
    /// Specify a predicate that will be applied to the query
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="specificationBuilder"></param>
    /// <param name="whereExpression"></param>
    public static ISpecificationBuilder<T> Where<T>(
        this ISpecificationBuilder<T> specificationBuilder,
        Expression<Func<T, bool>> whereExpression)
    {
        var expression = new WhereExpression<T>(whereExpression);

        specificationBuilder.Specification.WhereExpressions.Add(expression);

        return specificationBuilder;
    }
}