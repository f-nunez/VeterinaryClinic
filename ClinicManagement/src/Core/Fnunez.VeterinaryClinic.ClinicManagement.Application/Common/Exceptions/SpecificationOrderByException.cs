namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;

public class SpecificationOrderByException : Exception
{
    public SpecificationOrderByException(string propertyName)
        : base($"Property not found in the specification to be ordered by {propertyName}.")
    {
    }
}