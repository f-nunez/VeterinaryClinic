using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Enums;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore.Specifications.Exceptions;

public class NotFoundIncludeExpressionTypeException : Exception
{
    private const string DefaultMessage =
        "Include expression type {0} not found by the evaluator.";

    public NotFoundIncludeExpressionTypeException(IncludeExpressionType type)
        : base(string.Format(DefaultMessage, type))
    {
    }
}