namespace Fnunez.VeterinaryClinic.ClinicManagement.Api.Settings;

public interface ICorsPolicySetting
{
    public string BlazorServerUrl { get; }
    public string IdentityServerUrl { get; }
}