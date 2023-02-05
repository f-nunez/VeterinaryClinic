namespace IdentityServerHost.Pages.Grants;

public class ViewModel
{
    public IEnumerable<GrantViewModel> Grants { get; set; } = null!;
}

public class GrantViewModel
{
    public string ClientId { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string ClientUrl { get; set; } = null!;
    public string ClientLogoUrl { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Created { get; set; }
    public DateTime? Expires { get; set; }
    public IEnumerable<string> IdentityGrantNames { get; set; } = null!;
    public IEnumerable<string> ApiGrantNames { get; set; } = null!;
}