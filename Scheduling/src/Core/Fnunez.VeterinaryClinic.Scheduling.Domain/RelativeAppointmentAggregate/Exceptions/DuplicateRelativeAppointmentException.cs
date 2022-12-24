namespace Fnunez.VeterinaryClinic.Scheduling.Domain.RelativeAppointmentAggregate.Exceptions;

public class DuplicateRelativeAppointmentException : ArgumentException
{
    public DuplicateRelativeAppointmentException(string message, string paramName)
        : base(message, paramName)
    {
    }
}