using System.Linq.Expressions;
using System.Reflection;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Enums;
using Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore.Specifications.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using IncludeExpression = Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Expressions.IncludeExpression;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore.Specifications.Evaluators;

public static class IncludeExpressionEvaluator
{
    public static IQueryable<R> ComputeIncludeExpressions<R>(
        IQueryable<R> query,
        ISpecification<R> specification) where R : class
    {
        foreach (var includeExpression in specification.IncludeExpressions)
            switch (includeExpression.IncludeExpressionType)
            {
                case IncludeExpressionType.Include:
                    query = BuildInclude<R>(query, includeExpression);
                    break;
                case IncludeExpressionType.ThenInclude:
                    query = BuildThenInclude<R>(query, includeExpression);
                    break;
                default:
                    throw new NotFoundIncludeExpressionTypeException(
                        includeExpression.IncludeExpressionType);
            }

        return query;
    }

    private static IQueryable<R> BuildInclude<R>(
        IQueryable query,
        IncludeExpression includeExpression)
    {
        if (includeExpression is null)
            throw new ArgumentNullException(nameof(includeExpression));

        var result = IncludeMethodInfo
            .MakeGenericMethod(includeExpression.EntityType, includeExpression.PropertyType)
            .Invoke(null, new object[] { query, includeExpression.Expression });

        if (result is null)
            throw new TargetException();

        return (IQueryable<R>)result;
    }

    private static IQueryable<R> BuildThenInclude<R>(
        IQueryable query,
        IncludeExpression includeExpression)
    {
        if (includeExpression is null)
            throw new ArgumentNullException(nameof(includeExpression));

        if (includeExpression.PreviousPropertyType is null)
            throw new ArgumentNullException(
                nameof(includeExpression.PreviousPropertyType));

        var result = (IsGenericEnumerable(includeExpression.PreviousPropertyType, out var previousPropertyType)
                  ? ThenIncludeAfterEnumerableMethodInfo
                  : ThenIncludeAfterReferenceMethodInfo)
                    .MakeGenericMethod(
                        includeExpression.EntityType,
                        previousPropertyType,
                        includeExpression.PropertyType
                    ).Invoke(null, new object[] { query, includeExpression.Expression, });

        if (result is null)
            throw new TargetException();

        return (IQueryable<R>)result;
    }

    private static readonly MethodInfo IncludeMethodInfo = typeof(EntityFrameworkQueryableExtensions)
        .GetTypeInfo().GetDeclaredMethods(nameof(EntityFrameworkQueryableExtensions.Include))
        .Single(mi => mi.GetGenericArguments().Length == 2
            && mi.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IQueryable<>)
            && mi.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>));

    private static readonly MethodInfo ThenIncludeAfterReferenceMethodInfo
        = typeof(EntityFrameworkQueryableExtensions)
            .GetTypeInfo().GetDeclaredMethods(nameof(EntityFrameworkQueryableExtensions.ThenInclude))
            .Single(mi => mi.GetGenericArguments().Length == 3
                && mi.GetParameters()[0].ParameterType.GenericTypeArguments[1].IsGenericParameter
                && mi.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IIncludableQueryable<,>)
                && mi.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>));

    private static readonly MethodInfo ThenIncludeAfterEnumerableMethodInfo
        = typeof(EntityFrameworkQueryableExtensions)
            .GetTypeInfo().GetDeclaredMethods(nameof(EntityFrameworkQueryableExtensions.ThenInclude))
            .Where(mi => mi.GetGenericArguments().Length == 3)
            .Single(
                mi =>
                {
                    var typeInfo = mi.GetParameters()[0].ParameterType.GenericTypeArguments[1];

                    return typeInfo.IsGenericType
                        && typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                        && mi.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IIncludableQueryable<,>)
                        && mi.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>);
                });

    private static Lazy<Func<IQueryable, LambdaExpression, IQueryable>> CreateIncludeDelegate((Type EntityType, Type PropertyType, Type? PreviousPropertyType) cacheKey)
        => new Lazy<Func<IQueryable, LambdaExpression, IQueryable>>(() =>
        {
            var concreteInclude = IncludeMethodInfo.MakeGenericMethod(cacheKey.EntityType, cacheKey.PropertyType);
            var sourceParameter = Expression.Parameter(typeof(IQueryable));
            var selectorParameter = Expression.Parameter(typeof(LambdaExpression));

            var call = Expression.Call(
                    concreteInclude,
                    Expression.Convert(sourceParameter, typeof(IQueryable<>).MakeGenericType(cacheKey.EntityType)),
                    Expression.Convert(selectorParameter, typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(cacheKey.EntityType, cacheKey.PropertyType))));

            var lambda = Expression.Lambda<Func<IQueryable, LambdaExpression, IQueryable>>(call, sourceParameter, selectorParameter);

            return lambda.Compile();
        });

    private static Lazy<Func<IQueryable, LambdaExpression, IQueryable>> CreateThenIncludeDelegate((Type EntityType, Type PropertyType, Type? PreviousPropertyType) cacheKey)
        => new Lazy<Func<IQueryable, LambdaExpression, IQueryable>>(() =>
        {
            if (cacheKey.PreviousPropertyType is null)
                throw new ArgumentNullException(nameof(cacheKey.PreviousPropertyType));

            MethodInfo thenIncludeInfo = ThenIncludeAfterReferenceMethodInfo;

            if (IsGenericEnumerable(cacheKey.PreviousPropertyType, out var previousPropertyType))
                thenIncludeInfo = ThenIncludeAfterEnumerableMethodInfo;

            var concreteThenInclude = thenIncludeInfo.MakeGenericMethod(cacheKey.EntityType, previousPropertyType, cacheKey.PropertyType);
            var sourceParameter = Expression.Parameter(typeof(IQueryable));
            var selectorParameter = Expression.Parameter(typeof(LambdaExpression));

            var call = Expression.Call(
                concreteThenInclude,
                Expression.Convert(
                    sourceParameter,
                    typeof(IIncludableQueryable<,>).MakeGenericType(cacheKey.EntityType, cacheKey.PreviousPropertyType)),
                    Expression.Convert(selectorParameter, typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(previousPropertyType, cacheKey.PropertyType))));

            var lambda = Expression.Lambda<Func<IQueryable, LambdaExpression, IQueryable>>(call, sourceParameter, selectorParameter);

            return lambda.Compile();
        });

    private static bool IsGenericEnumerable(Type type, out Type propertyType)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        {
            propertyType = type.GenericTypeArguments[0];
            return true;
        }

        propertyType = type;
        return false;
    }
}