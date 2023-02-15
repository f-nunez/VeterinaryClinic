using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Models.ApplicationUsers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public class SecurityService : ISecurityService
{
    public ApplicationUser? User { get; set; }
    private readonly NavigationManager _navigationManager;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public SecurityService(
        NavigationManager navigationManager,
        AuthenticationStateProvider authenticationStateProvider,
        HttpClient httpClient)
    {
        _navigationManager = navigationManager;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var result = await _authenticationStateProvider
            .GetAuthenticationStateAsync();

        if (result.User.Identity != null)
            return result.User.Identity.IsAuthenticated;

        return false;
    }

    public void Login()
    {
        _navigationManager.NavigateTo("bff/login", true);
    }

    public async Task Logout()
    {
        var result = await _authenticationStateProvider
            .GetAuthenticationStateAsync();

        string logoutUrl = result.User.FindFirst("bff:logout_url")?.Value!;

        _navigationManager.NavigateTo(logoutUrl, true);
    }

    public async Task SetUserInfoAsync()
    {
        var result = await _authenticationStateProvider
            .GetAuthenticationStateAsync();

        if (result.User.Identity is null)
            return;

        User = new User
        {
            Email = result.User.FindFirst("email")?.Value!,
            Id = result.User.FindFirst("sub")?.Value!,
            Name = result.User.Identity.Name!,
            Roles = result.User.FindFirst("role")?.Value!.Split(" ")!,
            Username = result.User.FindFirst("preferred_username")?.Value!
        };
    }
}