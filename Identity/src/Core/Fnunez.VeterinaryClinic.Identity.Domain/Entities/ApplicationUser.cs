using Microsoft.AspNetCore.Identity;

namespace Fnunez.VeterinaryClinic.Identity.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }

    public ApplicationUser()
    {
        Name = string.Empty;
    }
}