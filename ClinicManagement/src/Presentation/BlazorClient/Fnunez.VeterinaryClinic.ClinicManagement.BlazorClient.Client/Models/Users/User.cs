namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Models.Users;

public class User
{
    public string Email { get; set; } = default!;
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string[] Roles { get; set; } = default!;
    public string Username { get; set; } = default!;
}