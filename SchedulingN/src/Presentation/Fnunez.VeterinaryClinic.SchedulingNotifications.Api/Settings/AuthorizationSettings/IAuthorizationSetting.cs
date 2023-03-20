namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Api.Settings;

public interface IAuthorizationSetting
{
    Policy[] Policies { get; }
}