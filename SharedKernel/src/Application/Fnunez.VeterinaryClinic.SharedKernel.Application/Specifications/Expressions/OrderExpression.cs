using System.Linq.Expressions;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Enums;

namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Expressions;

public class OrderExpression<T>
{
    /// <summary>
    /// A function to extract a key from an element.
    /// </summary>
    public Expression<Func<T, object?>> KeySelector { get; }

    /// <summary>
    /// Whether to (subsequently) sort ascending or descending.
    /// </summary>
    public OrderExpressionType OrderExpressionType { get; }

    /// <summary>
    /// Creates instance of <see cref="OrderExpressionInfo{T}" />.
    /// </summary>
    /// <param name="keySelector">A function to extract a key from an element.</param>
    /// <param name="orderExpressionType">Whether to (subsequently) sort ascending or descending.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="keySelector"/> is null.</exception>
    public OrderExpression(
        Expression<Func<T, object?>> keySelector,
        OrderExpressionType orderExpressionType)
    {
        if (keySelector is null)
            throw new ArgumentNullException(nameof(keySelector));

        KeySelector = keySelector;
        OrderExpressionType = orderExpressionType;
    }
}