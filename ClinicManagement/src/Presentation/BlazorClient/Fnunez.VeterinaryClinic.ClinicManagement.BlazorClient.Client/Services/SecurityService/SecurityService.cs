using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Models.ApplicationUsers;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Settings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public class SecurityService : ISecurityService
{
    private readonly string _reverseProxyRoute;
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;
    public ApplicationUser? User { get; set; }

    public SecurityService(
        IBackendForFrontendSetting bffSetting,
        HttpClient httpClient,
        NavigationManager navigationManager)
    {
        _reverseProxyRoute = bffSetting.SuffixRouteForAccessToken;
        _httpClient = httpClient;
        _navigationManager = navigationManager;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        var result = await _httpClient.GetAsync($"{_reverseProxyRoute}/User/GetAccessToken");

        if (!result.IsSuccessStatusCode)
            return string.Empty;
        
        return await result.Content.ReadAsStringAsync();
    }

    public bool IsAuthenticated()
    {
        return User is not null;
    }

    public void Login()
    {
        _navigationManager.NavigateTo("bff/login", true);
    }

    public void Logout()
    {
        if (User is not null)
            _navigationManager.NavigateTo(User.LogoutUrl, true);
    }

    public void SetApplicationUser(AuthenticationState authenticationState)
    {
        if (authenticationState.User.Identity?.IsAuthenticated == true)
        {
            User = new ApplicationUser
            {
                Email = authenticationState.User.FindFirst("email")?.Value!,
                Id = authenticationState.User.FindFirst("sub")?.Value!,
                LogoutUrl = authenticationState.User.FindFirst("bff:logout_url")?.Value!,
                Name = authenticationState.User.Identity.Name!,
                Roles = authenticationState.User.FindFirst("role")?.Value!.Split(" ")!,
                Username = authenticationState.User.FindFirst("preferred_username")?.Value!
            };
        }
        else
        {
            User = null;
        }
    }
}