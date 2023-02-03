using Fnunez.VeterinaryClinic.Identity.Domain.Entities;
using Fnunez.VeterinaryClinic.Identity.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("Identity")
            );
        else
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("IdentityConnection"),
                    builder => builder.MigrationsAssembly(
                        typeof(ApplicationDbContext).Assembly.FullName
                    )
                )
            );

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddAspNetIdentity<ApplicationUser>()
            .AddConfigurationStore(configurationStoreOptions =>
            {
                configurationStoreOptions.ConfigureDbContext = options =>
                    options.UseInMemoryDatabase("IdentityServer");
            })
            .AddOperationalStore(operationStoreOptions =>
            {
                operationStoreOptions.ConfigureDbContext = options =>
                    options.UseInMemoryDatabase("IdentityServer");
            });
        else
            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddAspNetIdentity<ApplicationUser>()
            .AddConfigurationStore(configurationStoreOptions =>
            {
                configurationStoreOptions.ConfigureDbContext = options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("IdentityServerConnection"),
                        builder => builder.MigrationsAssembly(
                            typeof(ConfigurationStoreDbContext).Assembly.FullName
                        )
                    );
            })
            .AddOperationalStore(operationStoreOptions =>
            {
                operationStoreOptions.ConfigureDbContext = options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("IdentityServerConnection"),
                        UriBuilder => UriBuilder.MigrationsAssembly(
                            typeof(OperationalStoreDbContext).Assembly.GetName().Name
                        )
                    );
            });

        services.AddScoped<ConfigurationStoreDbContext>();

        services.AddScoped<OperationalStoreDbContext>();

        services.AddScoped<ApplicationDbContextSeeder>();

        services.AddScoped<ConfigurationStoreDbContextSeeder>();

        services.AddScoped<OperationalStoreDbContextSeeder>();

        services.AddAuthentication();

        services.AddRazorPages();

        return services;
    }
}