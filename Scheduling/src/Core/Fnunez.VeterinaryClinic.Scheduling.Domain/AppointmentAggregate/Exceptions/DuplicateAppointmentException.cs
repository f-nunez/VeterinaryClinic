namespace Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate.Exceptions;

public class DuplicateAppointmentException : ArgumentException
{
    public DuplicateAppointmentException(string message, string paramName)
        : base(message, paramName)
    {
    }
}