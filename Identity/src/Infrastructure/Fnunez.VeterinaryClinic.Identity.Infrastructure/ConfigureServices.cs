using Fnunez.VeterinaryClinic.Identity.Domain.Entities;
using Fnunez.VeterinaryClinic.Identity.Infrastructure.Persistence.Contexts;
using Fnunez.VeterinaryClinic.Identity.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        var identityServerSetting = configuration
            .GetSection(typeof(IdentityServerSetting).Name)
            .Get<IdentityServerSetting>()!;

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
                options.EmitStaticAudienceClaim = identityServerSetting.EmitStaticAudienceClaim;

                options.Events.RaiseErrorEvents = identityServerSetting.RaiseErrorEvents;
                options.Events.RaiseFailureEvents = identityServerSetting.RaiseFailureEvents;
                options.Events.RaiseInformationEvents = identityServerSetting.RaiseInformationEvents;
                options.Events.RaiseSuccessEvents = identityServerSetting.RaiseSuccessEvents;

                if (!string.IsNullOrEmpty(identityServerSetting.IssuerUri))
                    options.IssuerUri = identityServerSetting.IssuerUri;
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
                options.EmitStaticAudienceClaim = identityServerSetting.EmitStaticAudienceClaim;

                options.Events.RaiseErrorEvents = identityServerSetting.RaiseErrorEvents;
                options.Events.RaiseFailureEvents = identityServerSetting.RaiseFailureEvents;
                options.Events.RaiseInformationEvents = identityServerSetting.RaiseInformationEvents;
                options.Events.RaiseSuccessEvents = identityServerSetting.RaiseSuccessEvents;

                if (!string.IsNullOrEmpty(identityServerSetting.IssuerUri))
                    options.IssuerUri = identityServerSetting.IssuerUri;
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