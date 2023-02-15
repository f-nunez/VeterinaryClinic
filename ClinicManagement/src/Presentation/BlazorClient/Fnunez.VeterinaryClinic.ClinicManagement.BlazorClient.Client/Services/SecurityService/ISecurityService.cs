using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Models.ApplicationUsers;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public interface ISecurityService
{
    public ApplicationUser? User { get; }
    public Task<bool> IsAuthenticatedAsync();
    public void Login();
    public Task Logout();
    public Task SetUserInfoAsync();
}