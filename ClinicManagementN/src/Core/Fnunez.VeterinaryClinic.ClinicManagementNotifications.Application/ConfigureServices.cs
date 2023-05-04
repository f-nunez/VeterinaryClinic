using System.Reflection;
using FluentValidation;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Behaviors;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<INotificationEngineService, NotificationEngineService>();

        services.AddScoped<INotificationRequestFactory, NotificationRequestFactory>();

        services.AddScoped<IPayloadFactory, PayloadFactory>();

        return services;
    }
}