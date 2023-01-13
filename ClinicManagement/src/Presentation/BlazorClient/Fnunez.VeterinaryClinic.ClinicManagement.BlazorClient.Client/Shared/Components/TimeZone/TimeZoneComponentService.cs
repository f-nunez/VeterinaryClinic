namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.TimeZone;

public class TimeZoneComponentService : ITimeZoneComponentService
{
    private TimeZone _timeZone { get; set; } = null!;
    private readonly ITimeZoneComponentData _timeZoneComponentData;

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
        TimeZone? timeZone = _timeZoneComponentData.TimeZones
            .FirstOrDefault(tz => tz.Id == timeZoneId);

        if (timeZone is null)
            throw new ArgumentException($"TimeZone id: ({timeZoneId}) not found. Please clean your Browser's cache.");

        _timeZone = timeZone;
    }
}