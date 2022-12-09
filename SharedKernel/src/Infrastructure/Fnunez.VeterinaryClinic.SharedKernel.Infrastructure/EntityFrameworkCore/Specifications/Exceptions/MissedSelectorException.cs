namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore.Specifications.Exceptions;

public class MissedSelectorException : Exception
{
    private const string DefaultMessage =
        "The specification requires a Selector for projection result.";

    public MissedSelectorException() : base(DefaultMessage)
    {
    }
}