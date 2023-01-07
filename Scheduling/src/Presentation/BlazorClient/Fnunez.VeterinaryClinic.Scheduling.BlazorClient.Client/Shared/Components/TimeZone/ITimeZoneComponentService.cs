namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.TimeZone;

public interface ITimeZoneComponentService
{
    public TimeZone GetTimeZone(string timeZoneId);
    public IReadOnlyList<TimeZone> GetTimeZones();
    public void SetTimeZone(string timeZoneId);
}