namespace Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate.Exceptions;

public class AppointmentDateRangeException : Exception
{
    public AppointmentDateRangeException(
        DateTimeOffset startOn,
        DateTimeOffset endOn)
        : base($"StartOn: ({startOn.ToString()}) must be less than EndOn: {endOn.ToString()}")
    {
    }
}