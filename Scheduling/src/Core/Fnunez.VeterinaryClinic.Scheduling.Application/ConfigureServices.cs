using System.Reflection;
using FluentValidation;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Behaviors;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.Factories;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest;
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

        services.AddScoped<IEmailRequestService, EmailRequestService>();

        services.AddScoped<IIntegrationEventFactory, IntegrationEventFactory>();

        services.AddScoped<IIntegrationEventReceiverService, IntegrationEventReceiverService>();

        services.AddScoped<INotificationRequestService, NotificationRequestService>();

        return services;
    }
}