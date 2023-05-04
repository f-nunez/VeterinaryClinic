namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Settings;

public interface IBackendForFrontendSetting
{
    int SecondsToCheckAuthenticationStateDuetime { get; }
    int SecondsToCheckAuthenticationStatePeriod { get; }
    int SecondsToRefreshUserCache { get; }
    string SuffixRouteForAccessToken { get; }
    string SuffixRouteForSchedulingApi { get; }
    string SuffixRouteForSchedulingNotificationsApi { get; }
    string SuffixRouteForSchedulingNotificationsHub { get; }
}