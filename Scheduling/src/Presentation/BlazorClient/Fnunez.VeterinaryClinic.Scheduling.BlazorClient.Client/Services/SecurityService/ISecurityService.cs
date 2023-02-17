using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Models.ApplicationUsers;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public interface ISecurityService
{
    public ApplicationUser? User { get; }
    public Task<bool> IsAuthenticatedAsync();
    public void Login();
    public Task LogoutAsync();
    public Task SetApplicationUserAsync();
}