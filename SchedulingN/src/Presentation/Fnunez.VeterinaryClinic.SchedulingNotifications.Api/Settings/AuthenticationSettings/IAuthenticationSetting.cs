namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Api.Settings;

public interface IAuthenticationSetting
{
    string Audience { get; }
    string Authority { get; }
    string DefaultScheme { get; }
    bool RequireHttpsMetadata { get; }
    bool ValidateAudience { get; }
}