using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Fnunez.VeterinaryClinic.Identity.Infrastructure.Persistence.Contexts;

public class ConfigurationStoreDbContext : ConfigurationDbContext
{
    public ConfigurationStoreDbContext(
        DbContextOptions<ConfigurationDbContext> options)
        : base(options)
    {
    }
}