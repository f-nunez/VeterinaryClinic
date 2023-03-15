using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Models.ApplicationUsers;
using Microsoft.AspNetCore.Components.Authorization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public interface ISecurityService
{
    ApplicationUser? User { get; }
    bool IsAuthenticated();
    void Login();
    void Logout();
    void SetApplicationUser(AuthenticationState authenticationState);
}