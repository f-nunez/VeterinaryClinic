namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Models.ApplicationUsers;

public class ApplicationUser
{
    public string Email { get; set; } = default!;
    public string Id { get; set; } = default!;
    public string LogoutUrl { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string[] Roles { get; set; } = default!;
    public string Username { get; set; } = default!;
}