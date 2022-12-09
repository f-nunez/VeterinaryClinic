using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Enums;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Expressions;
using Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore.Specifications.Exceptions;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore.Specifications.Evaluators;

public static class OrderExpressionEvaluator
{
    public static IQueryable<R> ComputeOrderExpressions<R>(
        IQueryable<R> query,
        ISpecification<R> specification) where R : class
    {
        ValidateOrderExpressions(query, specification);

        IOrderedQueryable<R>? orderedQuery = GetOrderedQuery(query, specification);

        if (orderedQuery != null)
            query = orderedQuery;

        return query;
    }

    private static void ValidateOrderExpressions<R>(
        IQueryable<R> query,
        ISpecification<R> specification) where R : class
    {
        int ascAndDescOrderExpressionCount = specification.OrderExpressions
            .Count(x =>
                x.OrderExpressionType == OrderExpressionType.OrderBy
                ||
                x.OrderExpressionType == OrderExpressionType.OrderByDescending);

        bool hasMoreThanOneOrderExpression = ascAndDescOrderExpressionCount > 1;

        if (hasMoreThanOneOrderExpression)
            throw new DuplicatedOrderExpressionException();
    }

    private static IOrderedQueryable<R>? GetOrderedQuery<R>(
        IQueryable<R> query,
        ISpecification<R> specification) where R : class
    {
        IOrderedQueryable<R>? orderedQuery = null;

        foreach (var orderExpression in specification.OrderExpressions)
            orderedQuery = GetOrderedQueryByOrderType(query, orderExpression);

        return orderedQuery;
    }

    private static IOrderedQueryable<R>? GetOrderedQueryByOrderType<R>(
        IQueryable<R> query,
        OrderExpression<R> orderExpression) where R : class
    {
        IOrderedQueryable<R>? orderedQuery = null;
        switch (orderExpression.OrderExpressionType)
        {
            case OrderExpressionType.OrderBy:
                orderedQuery = query.OrderBy(orderExpression.KeySelector);
                break;

            case OrderExpressionType.OrderByDescending:
                orderedQuery = query.OrderByDescending(orderExpression.KeySelector);
                break;

            case OrderExpressionType.ThenBy:
                if (orderedQuery is null)
                    throw new ArgumentNullException(nameof(orderedQuery));

                orderedQuery = orderedQuery.ThenBy(orderExpression.KeySelector);
                break;

            case OrderExpressionType.ThenByDescending:
                if (orderedQuery is null)
                    throw new ArgumentNullException(nameof(orderedQuery));

                orderedQuery = orderedQuery.ThenByDescending(orderExpression.KeySelector);
                break;

            default:
                throw new NotFoundOrderExpressionTypeException(
                    orderExpression.OrderExpressionType);
        }

        return orderedQuery;
    }
}