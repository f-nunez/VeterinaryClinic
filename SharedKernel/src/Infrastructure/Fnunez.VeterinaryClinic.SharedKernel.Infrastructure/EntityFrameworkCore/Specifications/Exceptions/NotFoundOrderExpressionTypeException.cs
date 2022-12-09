using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Enums;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore.Specifications.Exceptions;

public class NotFoundOrderExpressionTypeException : Exception
{
    private const string DefaultMessage =
        "Order expression type {0} not found by the evaluator.";

    public NotFoundOrderExpressionTypeException(OrderExpressionType type)
        : base(string.Format(DefaultMessage, type))
    {
    }
}