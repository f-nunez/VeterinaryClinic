namespace Fnunez.VeterinaryClinic.ClinicManagement.Api.Settings;

public interface IAuthorizationSetting
{
    public Policy[] Policies { get; }
}