namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Api.Settings;

public class CorsPolicySetting : ICorsPolicySetting
{
    public string BlazorServerUrl { get; set; } = null!;
    public string IdentityServerUrl { get; set; } = null!;
}