namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.TimeZone;

public class TimeZoneComponentData : ITimeZoneComponentData
{
    public IReadOnlyList<TimeZone> TimeZones { get; set; } = default!;
}