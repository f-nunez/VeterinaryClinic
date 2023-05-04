namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Exceptions;

public class SpecificationOrderByException : Exception
{
    public SpecificationOrderByException(string propertyName)
        : base($"Property not found in the specification to be ordered by {propertyName}.")
    {
    }
}