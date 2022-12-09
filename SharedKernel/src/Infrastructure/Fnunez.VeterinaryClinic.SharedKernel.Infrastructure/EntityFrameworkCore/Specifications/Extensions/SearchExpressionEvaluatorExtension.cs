using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore.Specifications.Extensions;

public static class SearchExpressionEvaluatorExtension
{
    private static readonly MethodInfo LikeMethodInfo = typeof(
        DbFunctionsExtensions).GetMethod(
            nameof(DbFunctionsExtensions.Like),
            new Type[] {
                typeof(DbFunctions),
                typeof(string),
                typeof(string)
            })
        ?? throw new TargetException("The EF.Functions.Like not found");

    private static readonly MemberExpression Functions = Expression
        .Property(null, typeof(EF).GetProperty(nameof(EF.Functions))
        ?? throw new TargetException("The EF.Functions not found!"));

    /// <summary>
    /// Filters <paramref name="source"/> by applying an 'SQL LIKE' operation to it.
    /// </summary>
    /// <typeparam name="T">The type being queried against.</typeparam>
    /// <param name="source">The sequence of <typeparamref name="T"/></param>
    /// <param name="criterias">
    /// <list type="bullet">
    ///     <item>Selector, the property to apply the SQL LIKE against.</item>
    ///     <item>SearchTerm, the value to use for the SQL LIKE.</item>
    /// </list>
    /// </param>
    /// <returns></returns>
    public static IQueryable<T> Search<T>(
        this IQueryable<T> source,
        IEnumerable<SearchExpression<T>> criterias)
    {
        Expression? expr = null;
        var parameter = Expression.Parameter(typeof(T), "x");

        foreach (var criteria in criterias)
        {
            if (string.IsNullOrEmpty(criteria.SearchTerm))
                continue;

            var propertySelector = ExpressionVisitorExtension.Replace(
                criteria.Selector,
                criteria.Selector.Parameters[0],
                parameter
            ) as LambdaExpression;

            if (propertySelector is null)
                throw new InvalidExpressionException();

            var likeExpression = Expression.Call(
                null,
                LikeMethodInfo,
                Functions,
                propertySelector.Body,
                Expression.Constant(criteria.SearchTerm)
            );

            expr = expr == null
                ? (Expression)likeExpression
                : Expression.OrElse(expr, likeExpression);
        }

        return expr == null
            ? source
            : source.Where(Expression.Lambda<Func<T, bool>>(expr, parameter));
    }
}