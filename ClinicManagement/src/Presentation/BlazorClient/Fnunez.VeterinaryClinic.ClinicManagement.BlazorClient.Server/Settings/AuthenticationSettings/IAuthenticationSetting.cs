namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Server.Settings;

public interface IAuthenticationSetting
{
    public string DefaultChallengeScheme { get; }
    public string DefaultScheme { get; }
    public string DefaultSignOutScheme { get; }
}