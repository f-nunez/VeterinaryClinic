namespace Fnunez.VeterinaryClinic.ClinicManagement.Api.Settings;

public interface IAuthenticationSetting
{
    public string Audience { get; }
    public string Authority { get; }
    public string DefaultScheme { get; }
    public bool ValidateAudience { get; }
}