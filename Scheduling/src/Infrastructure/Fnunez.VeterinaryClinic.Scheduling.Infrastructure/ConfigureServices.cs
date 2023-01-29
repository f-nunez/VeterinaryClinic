using Fnunez.VeterinaryClinic.Scheduling.Application.Interfaces.ServiceBus;
using Fnunez.VeterinaryClinic.Scheduling.Application.Interfaces.Services;
using Fnunez.VeterinaryClinic.Scheduling.Application.Interfaces.Settings;
using Fnunez.VeterinaryClinic.Scheduling.Infrastructure.Persistence.Contexts;
using Fnunez.VeterinaryClinic.Scheduling.Infrastructure.Persistence.Repositories;
using Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus;
using Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Observers;
using Fnunez.VeterinaryClinic.Scheduling.Infrastructure.Services;
using Fnunez.VeterinaryClinic.Scheduling.Infrastructure.Settings;
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
                options.UseInMemoryDatabase("Scheduling")
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

        services.AddSingleton<IClientStorageSetting>(configuration
            .GetSection(typeof(ClientStorageSetting).Name)
            .Get<ClientStorageSetting>()!);

        services.AddSingleton<IRabbitMqSetting>(configuration
            .GetSection(typeof(RabbitMqSetting).Name)
            .Get<RabbitMqSetting>()!);

        services.AddScoped<IFileSystemReaderService, FileSystemReaderService>();

        services.AddScoped<IServiceBus, MassTransitServiceBus>();

        services.AddConsumeObserver<LoggingConsumeObserver>();

        services.AddPublishObserver<LoggingPublishObserver>();

        services.AddReceiveObserver<LoggingReceiveObserver>();

        services.AddSendObserver<LoggingSendObserver>();

        return services;
    }
}