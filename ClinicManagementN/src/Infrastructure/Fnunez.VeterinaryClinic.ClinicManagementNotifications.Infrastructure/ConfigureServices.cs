using System.Reflection;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Settings;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Infrastructure.Persistence.Contexts;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Infrastructure.Persistence.Repositories;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Infrastructure.ServiceBus;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Infrastructure.ServiceBus.Observers;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Infrastructure.Settings;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MassTransit;
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
                options.UseInMemoryDatabase("ClinicManagementNotifications")
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

        services.AddSingleton<IRabbitMqSetting>(configuration
            .GetSection(typeof(RabbitMqSetting).Name)
            .Get<RabbitMqSetting>()!);

        services.AddScoped<IServiceBus, MassTransitServiceBus>();

        services.AddConsumeObserver<LoggingConsumeObserver>();

        services.AddPublishObserver<LoggingPublishObserver>();

        services.AddReceiveObserver<LoggingReceiveObserver>();

        services.AddSendObserver<LoggingSendObserver>();

        services.AddMassTransit(mt =>
        {
            mt.AddConsumers(Assembly.GetExecutingAssembly());

            mt.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqSetting = context
                    .GetRequiredService<IRabbitMqSetting>();

                cfg.Host(rabbitMqSetting.HostAddress);

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}