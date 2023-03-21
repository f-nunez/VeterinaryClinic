namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Settings;

public class BackendForFrontendSetting : IBackendForFrontendSetting
{
    public int SecondsToCheckAuthenticationStateDuetime { get; set; }
    public int SecondsToCheckAuthenticationStatePeriod { get; set; }
    public int SecondsToRefreshUserCache { get; set; }
    public string SuffixRouteForAccessToken { get; set; } = null!;
    public string SuffixRouteForSchedulingApi { get; set; } = null!;
    public string SuffixRouteForSchedulingNotificationsApi { get; set; } = null!;
    public string SuffixRouteForSchedulingNotificationsHub { get; set; } = null!;
}