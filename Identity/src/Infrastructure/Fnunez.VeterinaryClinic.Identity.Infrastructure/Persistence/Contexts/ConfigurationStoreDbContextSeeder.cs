using Duende.IdentityServer;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.Identity.Infrastructure.Persistence.Contexts;

public class ConfigurationStoreDbContextSeeder
{
    private readonly ILogger<ConfigurationStoreDbContext> _logger;
    private readonly ConfigurationStoreDbContext _context;

    public ConfigurationStoreDbContextSeeder(
        ILogger<ConfigurationStoreDbContext> logger,
        ConfigurationStoreDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task MigrateAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
                await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedDataAsync(string? environmentName)
    {
        try
        {
            await TrySeedAsync(environmentName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync(string? environmentName)
    {
        await AddIdentityResourcesAsync();

        await AddApiResourcesAsync();

        await AddApiScopesAsync();

        await AddClientsAsync(environmentName);
    }

    private async Task AddApiResourcesAsync()
    {
        if (await _context.ApiResources.AnyAsync())
            return;

        await _context.AddRangeAsync(
            new ApiResource
            {
                Name = "7e2593ba-e3cd-40e5-a50e-506877d0210e",
                DisplayName = "ClinicManagement Api",
                Scopes = new List<string> { "clinic_management_api" },
                UserClaims = new List<string>
                {
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Name,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.SessionId
                }
            }.ToEntity(),
            new ApiResource
            {
                Name = "09cb5cff-f1fa-4f5f-aa26-d39f9b63b0d6",
                DisplayName = "ClinicManagementNotifications Api",
                Scopes = new List<string> { "clinic_management_notifications_api" },
                UserClaims = new List<string>
                {
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.SessionId
                }
            }.ToEntity(),
            new ApiResource
            {
                Name = "87fd9858-661d-409e-810b-86055039bcce",
                DisplayName = "Scheduling Api",
                Scopes = new List<string> { "scheduling_api" },
                UserClaims = new List<string>
                {
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Name,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.SessionId
                }
            }.ToEntity(),
            new ApiResource
            {
                Name = "407acbe0-3063-427a-8501-770640d9913f",
                DisplayName = "SchedulingNotifications Api",
                Scopes = new List<string> { "scheduling_notifications_api" },
                UserClaims = new List<string>
                {
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.SessionId
                }
            }.ToEntity()
        );

        await _context.SaveChangesAsync();
    }

    private async Task AddApiScopesAsync()
    {
        if (await _context.ApiScopes.AnyAsync())
            return;

        await _context.AddRangeAsync(
            new ApiScope
            {
                Name = "clinic_management_api",
                DisplayName = "ClinicManagement Api",
                UserClaims = new[]
                {
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Name,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.SessionId
                }
            }.ToEntity(),
            new ApiScope
            {
                Name = "clinic_management_notifications_api",
                DisplayName = "ClinicManagementNotifications Api",
                UserClaims = new[]
                {
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.SessionId
                }
            }.ToEntity(),
            new ApiScope
            {
                Name = "scheduling_api",
                DisplayName = "Scheduling Api",
                UserClaims = new[]
                {
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Name,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.SessionId
                }
            }.ToEntity(),
            new ApiScope
            {
                Name = "scheduling_notifications_api",
                DisplayName = "SchedulingNotifications Api",
                UserClaims = new[]
                {
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.SessionId
                }
            }.ToEntity()
        );

        await _context.SaveChangesAsync();
    }

    private async Task AddIdentityResourcesAsync()
    {
        if (await _context.IdentityResources.AnyAsync())
            return;

        await _context.AddRangeAsync(
            new IdentityResources.OpenId().ToEntity(),
            new IdentityResources.Profile().ToEntity(),
            new IdentityResources.Email().ToEntity(),
            new IdentityResource
            {
                Name = "preferred_username",
                DisplayName = "PreferredUserName",
                UserClaims = { JwtClaimTypes.PreferredUserName }
            }.ToEntity(),
            new IdentityResource
            {
                Name = "roles",
                DisplayName = "Roles",
                UserClaims = { JwtClaimTypes.Role }
            }.ToEntity()
        );

        await _context.SaveChangesAsync();
    }

    private async Task AddClientsAsync(string? environmentName)
    {
        if (await _context.Clients.AnyAsync())
            return;

        if (string.IsNullOrEmpty(environmentName))
            environmentName = "Development";

        var clinicManagementBlazorApp = GetClientForClinicManagementApp(environmentName);

        var schedulingBlazorApp = GetClientForSchedulingApp(environmentName);

        await _context.AddRangeAsync(
            clinicManagementBlazorApp.ToEntity(),
            schedulingBlazorApp.ToEntity()
        );

        await _context.SaveChangesAsync();
    }

    private Client GetClientForClinicManagementApp(string environmentName)
    {
        List<string> allowedCorsOrigins = new();
        List<string> redirectUris = new();
        string backChannelLogoutUri;
        List<string> postLogoutRedirectUris = new();

        switch (environmentName)
        {
            case "DockerDevelopment":
                allowedCorsOrigins = new List<string> { "https://clinicmanagement-blazor.vc.local:7004" };
                redirectUris = new List<string> { "https://clinicmanagement-blazor.vc.local:7004/signin-oidc" };
                backChannelLogoutUri = "https://clinicmanagement-blazor.vc.local:7004/bff/backchannel";
                postLogoutRedirectUris = new List<string> { "https://clinicmanagement-blazor.vc.local:7004/signout-callback-oidc" };
                break;
            case "DockerNginx":
                allowedCorsOrigins = new List<string> { "https://clinicmanagement-blazor.vc.local" };
                redirectUris = new List<string> { "https://clinicmanagement-blazor.vc.local/signin-oidc" };
                backChannelLogoutUri = "https://clinicmanagement-blazor.vc.local/bff/backchannel";
                postLogoutRedirectUris = new List<string> { "https://clinicmanagement-blazor.vc.local/signout-callback-oidc" };
                break;
            case "Development":
            default:
                allowedCorsOrigins = new List<string> { "https://localhost:7004" };
                redirectUris = new List<string> { "https://localhost:7004/signin-oidc" };
                backChannelLogoutUri = "https://localhost:7004/bff/backchannel";
                postLogoutRedirectUris = new List<string> { "https://localhost:7004/signout-callback-oidc" };
                break;
        }

        return new Client
        {
            ClientId = "6c4c5801-1089-4c3c-83c7-ddc0eb3707b3",
            ClientSecrets = new List<Secret> { new("secret".Sha512()) },
            ClientName = "Clinic Management",
            Description = "ClinicManagement Blazor App",
            AllowedGrantTypes = GrantTypes.Code,
            AllowOfflineAccess = true,
            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.OfflineAccess,
                IdentityServerConstants.StandardScopes.Email,
                "preferred_username",
                "roles",
                "clinic_management_api",
                "clinic_management_notifications_api"
            },
            AllowedCorsOrigins = allowedCorsOrigins,
            RedirectUris = redirectUris,
            BackChannelLogoutUri = backChannelLogoutUri,
            PostLogoutRedirectUris = postLogoutRedirectUris,
            UpdateAccessTokenClaimsOnRefresh = true,
            RefreshTokenUsage = TokenUsage.ReUse
        };
    }

    private Client GetClientForSchedulingApp(string environmentName)
    {
        List<string> allowedCorsOrigins = new();
        List<string> redirectUris = new();
        string backChannelLogoutUri;
        List<string> postLogoutRedirectUris = new();

        switch (environmentName)
        {
            case "DockerDevelopment":
                allowedCorsOrigins = new List<string> { "https://scheduling-blazor.vc.local:7154" };
                redirectUris = new List<string> { "https://scheduling-blazor.vc.local:7154/signin-oidc" };
                backChannelLogoutUri = "https://scheduling-blazor.vc.local:7154/bff/backchannel";
                postLogoutRedirectUris = new List<string> { "https://scheduling-blazor.vc.local:7154/signout-callback-oidc" };
                break;
            case "DockerNginx":
                allowedCorsOrigins = new List<string> { "https://scheduling-blazor.vc.local" };
                redirectUris = new List<string> { "https://scheduling-blazor.vc.local/signin-oidc" };
                backChannelLogoutUri = "https://scheduling-blazor.vc.local/bff/backchannel";
                postLogoutRedirectUris = new List<string> { "https://scheduling-blazor.vc.local/signout-callback-oidc" };
                break;
            case "Development":
            default:
                allowedCorsOrigins = new List<string> { "https://localhost:7154" };
                redirectUris = new List<string> { "https://localhost:7154/signin-oidc" };
                backChannelLogoutUri = "https://localhost:7154/bff/backchannel";
                postLogoutRedirectUris = new List<string> { "https://localhost:7154/signout-callback-oidc" };
                break;
        }

        return new Client
        {
            ClientId = "85064410-c9c8-4afa-9deb-cb4b8e5114df",
            ClientSecrets = new List<Secret> { new("secret".Sha512()) },
            ClientName = "Scheduling",
            Description = "Scheduling Blazor App",
            AllowedGrantTypes = GrantTypes.Code,
            AllowOfflineAccess = true,
            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.OfflineAccess,
                IdentityServerConstants.StandardScopes.Email,
                "preferred_username",
                "roles",
                "scheduling_api",
                "scheduling_notifications_api"
            },
            AllowedCorsOrigins = allowedCorsOrigins,
            RedirectUris = redirectUris,
            BackChannelLogoutUri = backChannelLogoutUri,
            PostLogoutRedirectUris = postLogoutRedirectUris,
            UpdateAccessTokenClaimsOnRefresh = true,
            RefreshTokenUsage = TokenUsage.ReUse
        };
    }
}