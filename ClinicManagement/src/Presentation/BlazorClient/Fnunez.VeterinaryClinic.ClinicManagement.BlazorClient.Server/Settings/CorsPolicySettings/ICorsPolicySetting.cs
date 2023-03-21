namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Server.Settings;

public interface ICorsPolicySetting
{
    string ClinicManagementApiUrl { get; }
    string ClinicManagementNotificationsApiUrl { get; }
    string IdentityServerUrl { get; }
}