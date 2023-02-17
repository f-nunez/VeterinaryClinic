namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Settings;

public interface IBackendForFrontendSetting
{
    public string LocalEndpointToRouteRemoteApiByReverseProxy { get; }
    public int SecondsToCheckAuthenticationStateDuetime { get; }
    public int SecondsToCheckAuthenticationStatePeriod { get; }
    public int SecondsToRefreshUserCache { get; }
}