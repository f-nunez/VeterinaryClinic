namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Server.Settings;

public class CorsPolicySetting : ICorsPolicySetting
{
    public string ClinicManagementApiUrl { get; set; } = null!;
    public string ClinicManagementNotificationsApiUrl { get; set; } = null!;
    public string IdentityServerUrl { get; set; } = null!;
}