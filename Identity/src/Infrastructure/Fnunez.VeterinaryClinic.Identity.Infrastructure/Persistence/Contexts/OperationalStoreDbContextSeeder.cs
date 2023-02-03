using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.Identity.Infrastructure.Persistence.Contexts;

public class OperationalStoreDbContextSeeder
{
    private readonly ILogger<OperationalStoreDbContext> _logger;
    private readonly OperationalStoreDbContext _context;

    public OperationalStoreDbContextSeeder(
        ILogger<OperationalStoreDbContext> logger,
        OperationalStoreDbContext context)
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
}