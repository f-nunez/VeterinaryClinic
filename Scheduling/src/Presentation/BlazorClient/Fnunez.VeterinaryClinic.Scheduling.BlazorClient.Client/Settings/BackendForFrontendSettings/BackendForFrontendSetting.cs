namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Settings;

public class BackendForFrontendSetting : IBackendForFrontendSetting
{
    public string LocalEndpointToRouteRemoteApiByReverseProxy { get; set; } = null!;
    public int SecondsToCheckAuthenticationStateDuetime { get; set; }
    public int SecondsToCheckAuthenticationStatePeriod { get; set; }
    public int SecondsToRefreshUserCache { get; set; }
}