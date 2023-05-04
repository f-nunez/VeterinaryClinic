namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.TimeZone;

public class TimeZone
{
    public string DisplayName { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public int UtcOffsetInMinutes { get; set; }
}