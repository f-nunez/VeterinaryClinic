namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore.Specifications.Exceptions;

public class DuplicatedOrderExpressionException : Exception
{
    private const string DefaultMessage =
        "The specification contains more than one Order expression.";

    public DuplicatedOrderExpressionException() : base(DefaultMessage)
    {
    }
}