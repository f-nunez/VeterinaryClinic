namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Settings;

public interface IBackendForFrontendSetting
{
    public int SecondsToCheckAuthenticationStateDuetime { get; }
    public int SecondsToCheckAuthenticationStatePeriod { get; }
    public int SecondsToRefreshUserCache { get; }
    public string SuffixRouteForClinicManagementApi { get; }
    public string SuffixRouteForClinicManagementNotificationsApi { get; }
}