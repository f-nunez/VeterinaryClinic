namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.TimeZone;

public class TimeZoneComponentService : ITimeZoneComponentService
{
    private readonly ITimeZoneComponentData _timeZoneComponentData;

    private TimeZone _timeZone { get; set; } = null!;

    public TimeZoneComponentService(ITimeZoneComponentData timeZoneComponentData)
    {
        _timeZoneComponentData = timeZoneComponentData;
    }

    public TimeZone GetTimeZone(string timeZoneId)
    {
        TimeZone? timeZone = _timeZoneComponentData.TimeZones
            .FirstOrDefault(tz => tz.Id == timeZoneId);

        if (timeZone != null)
            return timeZone;

        throw new ArgumentNullException(nameof(timeZone));
    }

    public IReadOnlyList<TimeZone> GetTimeZones()
    {
        return _timeZoneComponentData.TimeZones;
    }

    public void SetTimeZone(string timeZoneId)
    {
        var selectedTimeZone = _timeZoneComponentData.TimeZones
            .FirstOrDefault(tz => tz.Id == timeZoneId);

        if (selectedTimeZone is null)
            throw new ArgumentException($"TimeZone id: ({timeZoneId}) not found.");

        _timeZone = selectedTimeZone;
    }
}