using System.Reflection;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.EmailCompositions;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Payloads;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Requests;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped<IEmailCompositionFactory, EmailCompositionFactory>();

        services.AddScoped<IEmailEngineService, EmailEngineService>();

        services.AddScoped<IEmailRequestFactory, EmailRequestFactory>();

        services.AddScoped<IPayloadFactory, PayloadFactory>();

        return services;
    }
}