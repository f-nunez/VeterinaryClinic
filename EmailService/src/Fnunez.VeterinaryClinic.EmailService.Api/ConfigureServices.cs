using System.Reflection;
using Fnunez.VeterinaryClinic.EmailService.Api.ServiceBus;
using Fnunez.VeterinaryClinic.EmailService.Api.ServiceBus.Observers;
using Fnunez.VeterinaryClinic.EmailService.Api.Settings;
using MassTransit;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

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

        services.AddSingleton<IEmailSetting>(configuration
            .GetSection(typeof(EmailSetting).Name)
            .Get<EmailSetting>()!);

        return services;
    }

    public static WebApplication AddWebApplicationBuilder(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}