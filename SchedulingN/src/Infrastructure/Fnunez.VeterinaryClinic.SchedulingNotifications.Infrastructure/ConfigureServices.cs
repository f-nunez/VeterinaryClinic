using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Settings;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Infrastructure.Persistence.Contexts;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Infrastructure.Persistence.Repositories;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Infrastructure.Settings;
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
                options.UseInMemoryDatabase("SchedulingNotifications")
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

        return services;
    }
}