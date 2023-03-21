using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Models.ApplicationUsers;
using Microsoft.AspNetCore.Components.Authorization;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public interface ISecurityService
{
    ApplicationUser? User { get; }
    Task<string> GetAccessTokenAsync();
    bool IsAuthenticated();
    void Login();
    void Logout();
    void SetApplicationUser(AuthenticationState authenticationState);
}