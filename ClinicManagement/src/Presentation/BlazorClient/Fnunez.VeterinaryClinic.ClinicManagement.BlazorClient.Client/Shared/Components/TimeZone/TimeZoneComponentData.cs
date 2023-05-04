namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.TimeZone;

public class TimeZoneComponentData : ITimeZoneComponentData
{
    public IReadOnlyList<TimeZone> TimeZones { get; set; } = default!;
}