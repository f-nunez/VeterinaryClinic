using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Models.Users;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public interface ISecurityService
{
    public User? User { get; }
    public Task<bool> IsAuthenticatedAsync();
    public void Login();
    public Task Logout();
    public Task SetUserInfoAsync();
}