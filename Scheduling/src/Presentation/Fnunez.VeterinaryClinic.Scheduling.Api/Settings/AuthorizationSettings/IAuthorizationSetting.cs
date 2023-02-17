namespace Fnunez.VeterinaryClinic.Scheduling.Api.Settings;

public interface IAuthorizationSetting
{
    public Policy[] Policies { get; }
}