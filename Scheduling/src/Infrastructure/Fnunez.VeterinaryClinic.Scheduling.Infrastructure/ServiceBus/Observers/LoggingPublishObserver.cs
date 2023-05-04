using MassTransit;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Observers;

public class LoggingPublishObserver : IPublishObserver
{
    private readonly ILogger<LoggingPublishObserver> _logger;

    public LoggingPublishObserver(ILogger<LoggingPublishObserver> logger)
    {
        _logger = logger;
    }

    public async Task PostPublish<T>(PublishContext<T> context) where T : class
    {
        await Task.Yield();

        _logger.LogInformation(
            "PostPublish Message: {Message}, CorrelationId: {CorrelationId}, Namespace: {Namespace}",
            typeof(T).Name,
            context.CorrelationId,
            typeof(T).Namespace
        );
    }

    public async Task PrePublish<T>(PublishContext<T> context) where T : class
    {
        await Task.Yield();

        _logger.LogInformation(
            "PrePublish Message: {Message}, CorrelationId: {CorrelationId}, Namespace: {Namespace}",
            typeof(T).Name,
            context.CorrelationId,
            typeof(T).Namespace
        );
    }

    public async Task PublishFault<T>(
        PublishContext<T> context,
        Exception exception) where T : class
    {
        await Task.Yield();

        _logger.LogError(
            "PublishFault Message: {Message}, CorrelationId: {CorrelationId}, Namespace: {Namespace}, Error: {Error}",
            typeof(T).Name,
            context.CorrelationId,
            typeof(T).Namespace,
            exception.Message
        );
    }
}