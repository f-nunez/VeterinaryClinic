namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Api.Settings;

public interface IAuthorizationSetting
{
    Policy[] Policies { get; }
}