using System.Reflection;
using FluentValidation;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Behaviors;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Requests;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());

            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));

            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.AddScoped<INotificationEngineService, NotificationEngineService>();

        services.AddScoped<INotificationRequestFactory, NotificationRequestFactory>();

        services.AddScoped<IPayloadFactory, PayloadFactory>();

        return services;
    }
}