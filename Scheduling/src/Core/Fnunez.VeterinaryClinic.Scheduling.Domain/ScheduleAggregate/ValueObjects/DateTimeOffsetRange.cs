using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate.ValueObjects;

public class DateTimeOffsetRange : ValueObject
{
    public DateTimeOffset StartOn { get; private set; }
    public DateTimeOffset EndOn { get; private set; }

    public DateTimeOffsetRange(DateTimeOffset startOn, DateTimeOffset endOn)
    {
        ValidateOutOfRange(startOn, endOn);
        StartOn = startOn;
        EndOn = endOn;
    }

    public DateTimeOffsetRange(DateTimeOffset startOn, TimeSpan duration)
        : this(startOn, startOn.Add(duration))
    {
    }

    public DateTimeOffsetRange CreateNewDuration(TimeSpan newDuration)
    {
        return new DateTimeOffsetRange(StartOn, newDuration);
    }

    public DateTimeOffsetRange CreateNewEndOn(DateTimeOffset newEndOn)
    {
        return new DateTimeOffsetRange(StartOn, newEndOn);
    }

    public DateTimeOffsetRange CreateNewStartOn(DateTimeOffset newStartOn)
    {
        return new DateTimeOffsetRange(newStartOn, EndOn);
    }

    public DateTimeOffsetRange CreateOneDayRange(DateTimeOffset startOn)
    {
        return new DateTimeOffsetRange(startOn, startOn.AddDays(1.0));
    }

    public DateTimeOffsetRange CreateOneWeekRange(DateTimeOffset startOn)
    {
        return new DateTimeOffsetRange(startOn, startOn.AddDays(7.0));
    }

    public int DurationInMinutes()
    {
        double totalMinutes = (EndOn - StartOn).TotalMinutes;
        return (int)Math.Round(totalMinutes, 0);
    }

    public bool Overlaps(DateTimeOffsetRange dateTimeOffsetRange)
    {
        if (StartOn < dateTimeOffsetRange.EndOn)
            return EndOn > dateTimeOffsetRange.StartOn;

        return false;
    }

    private static void ValidateOutOfRange(
        DateTimeOffset startOn,
        DateTimeOffset endOn)
    {
        var comparer = Comparer<DateTimeOffset>.Default;

        if (comparer.Compare(startOn, endOn) > 0)
            throw new ArgumentException("StartOn should be less or equals than EndOn");

        if (comparer.Compare(endOn, startOn) < 0)
            throw new ArgumentException("EndOn should be greater or equals than StartOn");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StartOn;
        yield return EndOn;
    }
}