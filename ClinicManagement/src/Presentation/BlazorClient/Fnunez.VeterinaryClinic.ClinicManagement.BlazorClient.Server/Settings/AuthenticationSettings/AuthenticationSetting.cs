namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Server.Settings;

public class AuthenticationSetting : IAuthenticationSetting
{
    public string DefaultChallengeScheme { get; set; } = null!;
    public string DefaultScheme { get; set; } = null!;
    public string DefaultSignOutScheme { get; set; } = null!;
}