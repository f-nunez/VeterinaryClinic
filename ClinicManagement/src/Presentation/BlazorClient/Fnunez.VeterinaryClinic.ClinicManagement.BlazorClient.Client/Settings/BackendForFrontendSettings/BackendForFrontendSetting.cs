namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Settings;

public class BackendForFrontendSetting : IBackendForFrontendSetting
{
    public int SecondsToCheckAuthenticationStateDuetime { get; set; }
    public int SecondsToCheckAuthenticationStatePeriod { get; set; }
    public int SecondsToRefreshUserCache { get; set; }
    public string SuffixRouteForClinicManagementApi { get; set; } = null!;
    public string SuffixRouteForClinicManagementNotificationsApi { get; set; } = null!;
}