namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Server.Settings;

public class CorsPolicySetting : ICorsPolicySetting
{
    public string IdentityServerUrl { get; set; } = null!;
    public string SchedulingApiUrl { get; set; } = null!;
    public string SchedulingNotificationsApiUrl { get; set; } = null!;
}