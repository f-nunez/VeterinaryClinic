using Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.Settings;
using Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Persistence.Contexts;
using Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Persistence.Repositories;
using Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Settings;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("ClinicManagement")
            );
        else
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(
                        typeof(ApplicationDbContext).Assembly.FullName
                    )
                )
            );

        services.AddScoped<ApplicationDbContextSeeder>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IFileSystemDeleterService, FileSystemDeleterService>();

        services.AddScoped<IFileSystemReaderService, FileSystemReaderService>();

        services.AddScoped<IFileSystemWriterService, FileSystemWriterService>();

        services.AddSingleton<IClientStorageSetting>(configuration
            .GetSection(typeof(ClientStorageSetting).Name)
            .Get<ClientStorageSetting>()!);

        return services;
    }
}