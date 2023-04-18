namespace Fnunez.VeterinaryClinic.ClinicManagement.Api.Settings;

public class AuthenticationSetting : IAuthenticationSetting
{
    public string Audience { get; set; } = null!;
    public string Authority { get; set; } = null!;
    public string DefaultScheme { get; set; } = null!;
    public bool RequireHttpsMetadata { get; set; }
    public bool ValidateAudience { get; set; }
}