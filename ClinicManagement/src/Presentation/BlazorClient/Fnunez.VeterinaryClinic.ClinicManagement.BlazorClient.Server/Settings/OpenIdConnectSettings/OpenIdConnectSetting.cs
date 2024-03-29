namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Server.Settings;

public class OpenIdConnectSetting : IOpenIdConnectSetting
{
    public string AccessDeniedPath { get; set; } = null!;
    public string AuthenticationScheme { get; set; } = null!;
    public string Authority { get; set; } = null!;
    public string[] ClaimActionsToMap { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public bool EnabledGetClaimsFromUserInfoEndpoint { get; set; }
    public bool EnabledMapInboundClaims { get; set; }
    public bool EnabledRequireHttpsMetadata { get; set; }
    public bool EnabledSaveTokens { get; set; }
    public bool EnabledUsePkce { get; set; }
    public bool EnabledValidateAudience { get; set; }
    public string MetadataAddress { get; set; } = null!;
    public string ResponseMode { get; set; } = null!;
    public string ResponseType { get; set; } = null!;
    public string[] Scopes { get; set; } = null!;
}