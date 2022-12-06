namespace Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate.Exceptions;

public class DuplicateAppointmentException : ArgumentException
{
    public DuplicateAppointmentException(string message, string paramName)
        : base(message, paramName)
    {
    }
}