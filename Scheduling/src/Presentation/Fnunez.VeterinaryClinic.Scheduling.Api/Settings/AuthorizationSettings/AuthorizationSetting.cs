namespace Fnunez.VeterinaryClinic.Scheduling.Api.Settings;

public class AuthorizationSetting : IAuthorizationSetting
{
    public Policy[] Policies { get; set; } = null!;
}

public class Policy
{
    public string Name { get; set; } = null!;
    public bool RequireAuthenticatedUser { get; set; }
    public RequiredClaim[]? RequiredClaims { get; set; }
    public string[]? RequiredRoles { get; set; }
}

public class RequiredClaim
{
    public string ClaimType { get; set; } = null!;
    public string[] Values { get; set; } = null!;
}