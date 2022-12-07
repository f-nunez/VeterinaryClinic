using System.Linq.Expressions;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Enums;

namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Expressions;

public class IncludeExpression
{
    /// <summary>
    /// If <see cref="IncludeExpressionType" /> is <see cref="IncludeExpressionType.Include" />, represents a related entity that should be included.<para />
    /// If <see cref="IncludeExpressionType" /> is <see cref="IncludeExpressionType.ThenInclude" />, represents a related entity that should be included as part of the previously included entity.
    /// </summary>
    public LambdaExpression Expression { get; }

    /// <summary>
    /// The type of the source entity.
    /// </summary>
    public Type EntityType { get; }

    /// <summary>
    /// The include type.
    /// </summary>
    public IncludeExpressionType IncludeExpressionType { get; }

    /// <summary>
    /// The type of the included entity.
    /// </summary>
    public Type PropertyType { get; }

    /// <summary>
    /// The type of the previously included entity.
    /// </summary>
    public Type? PreviousPropertyType { get; }

    private IncludeExpression(
        LambdaExpression expression,
        Type entityType,
        Type propertyType,
        Type? previousPropertyType,
        IncludeExpressionType includeExpressionType)
    {
        _ = expression ?? throw new ArgumentNullException(nameof(expression));
        _ = entityType ?? throw new ArgumentNullException(nameof(entityType));
        _ = propertyType ?? throw new ArgumentNullException(nameof(propertyType));

        if (includeExpressionType == IncludeExpressionType.ThenInclude)
            _ = previousPropertyType ?? throw new ArgumentNullException(nameof(previousPropertyType));

        Expression = expression;
        EntityType = entityType;
        PropertyType = propertyType;
        PreviousPropertyType = previousPropertyType;
        IncludeExpressionType = includeExpressionType;
    }

    /// <summary>
    /// Creates instance of <see cref="IncludeExpressionInfo" /> which describes 'Include' query part.<para />
    /// Source (entityType) -> Include (propertyType).
    /// </summary>
    /// <param name="expression">The expression represents a related entity that should be included.</param>
    /// <param name="entityType">The type of the source entity.</param>
    /// <param name="propertyType">The type of the included entity.</param>
    public IncludeExpression(
        LambdaExpression expression,
        Type entityType,
        Type propertyType)
        : this(
            expression,
            entityType,
            propertyType,
            null,
            IncludeExpressionType.Include)
    {
    }

    /// <summary>
    /// Creates instance of <see cref="IncludeExpressionInfo" /> which describes 'ThenInclude' query part.<para />
    /// Source (entityType) -> Include (previousPropertyType) -> ThenInclude (propertyType).
    /// </summary>
    /// <param name="expression">The expression represents a related entity that should be included as part of the previously included entity.</param>
    /// <param name="entityType">The type of the source entity.</param>
    /// <param name="propertyType">The type of the included entity.</param>
    /// <param name="previousPropertyType">The type of the previously included entity.</param>
    public IncludeExpression(
        LambdaExpression expression,
        Type entityType,
        Type propertyType,
        Type previousPropertyType)
        : this(
            expression,
            entityType,
            propertyType,
            previousPropertyType,
            IncludeExpressionType.ThenInclude)
    {
    }
}