namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Api.Settings;

public interface ICorsPolicySetting
{
    public string BlazorServerUrl { get; }
    public string IdentityServerUrl { get; }
}