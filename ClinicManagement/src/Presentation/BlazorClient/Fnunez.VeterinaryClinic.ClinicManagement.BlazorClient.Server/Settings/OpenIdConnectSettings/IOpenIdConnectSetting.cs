namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Server.Settings;

public interface IOpenIdConnectSetting
{
    public string AuthenticationScheme { get; }
    public string Authority { get; }
    public string[] ClaimActionsToMap { get; }
    public string ClientId { get; }
    public string ClientSecret { get; }
    public bool EnabledGetClaimsFromUserInfoEndpoint { get; }
    public bool EnabledMapInboundClaims { get; }
    public bool EnabledSaveTokens { get; }
    public bool EnabledUsePkce { get; }
    public string ResponseMode { get; }
    public string ResponseType { get; }
    public string[] Scopes { get; }
}