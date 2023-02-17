namespace IdentityServerHost.Pages.Device;

public class ViewModel
{
    public string ClientName { get; set; } = null!;
    public string ClientUrl { get; set; } = null!;
    public string ClientLogoUrl { get; set; } = null!;
    public bool AllowRememberConsent { get; set; }

    public IEnumerable<ScopeViewModel> IdentityScopes { get; set; } = null!;
    public IEnumerable<ScopeViewModel> ApiScopes { get; set; } = null!;
}

public class ScopeViewModel
{
    public string Value { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool Emphasize { get; set; }
    public bool Required { get; set; }
    public bool Checked { get; set; }
}