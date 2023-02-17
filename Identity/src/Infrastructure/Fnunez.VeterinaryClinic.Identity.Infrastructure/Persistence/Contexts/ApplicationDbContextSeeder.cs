using System.Security.Claims;
using Fnunez.VeterinaryClinic.Identity.Domain.Entities;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.Identity.Infrastructure.Persistence.Contexts;

public class ApplicationDbContextSeeder
{
    private readonly ILogger<ApplicationDbContextSeeder> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationDbContextSeeder(
        ILogger<ApplicationDbContextSeeder> logger,
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public async Task MigrateAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
                await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedDataAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (!await _context.Users.AnyAsync())
        {
            var francisco = new ApplicationUser
            {
                Id = "9f79b45e-1ebe-4bb2-9d6f-e00da51b0848",
                Name = "francisco",
                Email = "francisco@nunez.ninja",
                UserName = "francisco"
            };

            await _userManager.CreateAsync(francisco, "P4$$w0rd");

            var ramon = new ApplicationUser
            {
                Id = "ca59b781-77c7-4b66-a05c-2910c2cb5d1f",
                Name = "ramon",
                Email = "ramon@nunez.ninja",
                UserName = "ramon"
            };

            await _userManager.CreateAsync(ramon, "P4$$w0rd");

            await _userManager.AddClaimsAsync(francisco, new Claim[]{
                new Claim(JwtClaimTypes.Name, "Francisco Nuñez"),
                new Claim(JwtClaimTypes.WebSite, "www.nunez.ninja"),
            });

            await _userManager.AddClaimsAsync(ramon, new Claim[]{
                new Claim(JwtClaimTypes.Name, "Ramon Nuñez"),
                new Claim(JwtClaimTypes.WebSite, "www.nunez.ninja"),
            });
        }

        if (!await _context.Roles.AnyAsync())
        {
            await _context.AddRangeAsync(
                new IdentityRole
                {
                    Id = "66cdc733-423d-4a46-8a5c-6c097cf4407a",
                    Name = "Manager",
                },
                new IdentityRole
                {
                    Id = "9fafd341-cf79-48f4-b56a-71fb6b7ecc14",
                    Name = "Staff",
                }
            );

            await _context.SaveChangesAsync();
        }

        if (!await _context.UserRoles.AnyAsync())
        {
            await _context.UserRoles.AddRangeAsync(
                new IdentityUserRole<string>
                {
                    RoleId = "66cdc733-423d-4a46-8a5c-6c097cf4407a",
                    UserId = "9f79b45e-1ebe-4bb2-9d6f-e00da51b0848"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "9fafd341-cf79-48f4-b56a-71fb6b7ecc14",
                    UserId = "ca59b781-77c7-4b66-a05c-2910c2cb5d1f"
                }
            );

            await _context.SaveChangesAsync();
        }
    }
}