using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationRoleAggregate;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationUserAggregate;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationUserRoleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Infrastructure.Persistence.Contexts;

public class ApplicationDbContextSeeder
{
    private readonly ILogger<ApplicationDbContextSeeder> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextSeeder(
        ILogger<ApplicationDbContextSeeder> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
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
        if (!await _context.ApplicationUsers.AnyAsync())
        {
            await _context.ApplicationUsers.AddRangeAsync(GetApplicationUsers());
            await _context.SaveChangesAsync();
        }

        if (!await _context.ApplicationRoles.AnyAsync())
        {
            await _context.ApplicationRoles.AddRangeAsync(GetApplicationRoles());
            await _context.SaveChangesAsync();
        }

        if (!await _context.ApplicationUserRoles.AnyAsync())
        {
            await _context.ApplicationUserRoles.AddRangeAsync(GetApplicationUserRoles());
            await _context.SaveChangesAsync();
        }
    }

    private List<ApplicationRole> GetApplicationRoles()
    {
        return new List<ApplicationRole>
        {
            new ApplicationRole("66cdc733-423d-4a46-8a5c-6c097cf4407a", "Manager"),
            new ApplicationRole("9fafd341-cf79-48f4-b56a-71fb6b7ecc14", "Staff")
        };
    }

    private List<ApplicationUserRole> GetApplicationUserRoles()
    {
        return new List<ApplicationUserRole>
        {
            new ApplicationUserRole(Guid.NewGuid(), "66cdc733-423d-4a46-8a5c-6c097cf4407a", "9f79b45e-1ebe-4bb2-9d6f-e00da51b0848"),
            new ApplicationUserRole(Guid.NewGuid(), "9fafd341-cf79-48f4-b56a-71fb6b7ecc14", "ca59b781-77c7-4b66-a05c-2910c2cb5d1f")
        };
    }

    private List<ApplicationUser> GetApplicationUsers()
    {
        return new List<ApplicationUser>
        {
            new ApplicationUser("9f79b45e-1ebe-4bb2-9d6f-e00da51b0848", "Francisco Nuñez"),
            new ApplicationUser("ca59b781-77c7-4b66-a05c-2910c2cb5d1f", "Ramon Nuñez")
        };
    }
}