namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Server.Settings;

public interface ICorsPolicySetting
{
    string IdentityServerUrl { get; }
    string SchedulingApiUrl { get; }
    string SchedulingNotificationsApiUrl { get; }
}