namespace Fnunez.VeterinaryClinic.Scheduling.Api.Settings;

public interface IAuthenticationSetting
{
    public string Audience { get; }
    public string Authority { get; }
    public string DefaultScheme { get; }
    public bool RequireHttpsMetadata { get; }
    public bool ValidateAudience { get; }
}