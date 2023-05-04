namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Settings;

public interface IBackendForFrontendSetting
{
    int SecondsToCheckAuthenticationStateDuetime { get; }
    int SecondsToCheckAuthenticationStatePeriod { get; }
    int SecondsToRefreshUserCache { get; }
    string SuffixRouteForAccessToken { get; }
    string SuffixRouteForClinicManagementApi { get; }
    string SuffixRouteForClinicManagementNotificationsApi { get; }
    string SuffixRouteForClinicManagementNotificationsHub { get; }
}