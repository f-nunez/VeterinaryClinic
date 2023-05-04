namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;

public class SpecificationThenByException : Exception
{
    public SpecificationThenByException(string propertyName)
        : base($"Property not found in the specification to be ordered then by {propertyName}.")
    {
    }
}