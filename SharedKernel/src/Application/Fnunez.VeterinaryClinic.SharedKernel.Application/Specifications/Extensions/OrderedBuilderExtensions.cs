using System.Linq.Expressions;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Enums;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Expressions;

namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

public static class OrderedBuilderExtensions
{
    /// <summary>
    /// Specify the query result will be ordered then by <paramref name="orderExpression"/> in an ascending order
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="orderedBuilder"></param>
    /// <param name="orderExpression"></param>
    public static IOrderedSpecificationBuilder<T> ThenBy<T>(
        this IOrderedSpecificationBuilder<T> orderedBuilder,
        Expression<Func<T, object?>> orderExpression)
    {
        var expression = new OrderExpression<T>(
            orderExpression,
            OrderExpressionType.ThenBy
        );

        orderedBuilder.Specification.OrderExpressions.Add(expression);

        return orderedBuilder;
    }

    /// <summary>
    /// Specify the query result will be ordered then by <paramref name="orderExpression"/> in a descending order
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="orderedBuilder"></param>
    /// <param name="orderExpression"></param>
    public static IOrderedSpecificationBuilder<T> ThenByDescending<T>(
        this IOrderedSpecificationBuilder<T> orderedBuilder,
        Expression<Func<T, object?>> orderExpression)
    {
        var expression = new OrderExpression<T>(
            orderExpression,
            OrderExpressionType.ThenByDescending
        );

        orderedBuilder.Specification.OrderExpressions.Add(expression);

        return orderedBuilder;
    }
}