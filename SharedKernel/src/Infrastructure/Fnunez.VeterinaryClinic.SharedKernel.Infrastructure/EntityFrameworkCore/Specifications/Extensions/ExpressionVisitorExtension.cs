using System.Linq.Expressions;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore.Specifications.Extensions;

internal class ExpressionVisitorExtension : ExpressionVisitor
{
    private readonly Expression _newExpression;
    private readonly ParameterExpression _oldParameter;

    private ExpressionVisitorExtension(
        ParameterExpression oldParameter,
        Expression newExpression)
    {
        _oldParameter = oldParameter;
        _newExpression = newExpression;
    }

    internal static Expression Replace(
        Expression expression,
        ParameterExpression oldParameter,
        Expression newExpression)
    {
        return new ExpressionVisitorExtension(oldParameter, newExpression)
            .Visit(expression);
    }

    protected override Expression VisitParameter(ParameterExpression parameter)
    {
        if (parameter == _oldParameter)
            return _newExpression;
        else
            return parameter;
    }
}