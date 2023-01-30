using MassTransit;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Observers;

public class LoggingConsumeObserver : IConsumeObserver
{
    private readonly ILogger<LoggingConsumeObserver> _logger;

    public LoggingConsumeObserver(ILogger<LoggingConsumeObserver> logger)
    {
        _logger = logger;
    }

    public async Task ConsumeFault<T>(
        ConsumeContext<T> context,
        Exception exception) where T : class
    {
        await Task.Yield();

        _logger.LogError(
            "ConsumeFault Message: {Message}, CorrelationId: {CorrelationId}, Namespace: {Namespace}, Error: {Error}",
            typeof(T).Name,
            context.CorrelationId,
            typeof(T).Namespace,
            exception.Message
        );
    }

    public async Task PostConsume<T>(ConsumeContext<T> context) where T : class
    {
        await Task.Yield();

        _logger.LogInformation(
            "PostConsume Message: {Message}, CorrelationId: {CorrelationId}, Namespace: {Namespace}",
            typeof(T).Name,
            context.CorrelationId,
            typeof(T).Namespace
        );
    }

    public async Task PreConsume<T>(ConsumeContext<T> context) where T : class
    {
        await Task.Yield();

        _logger.LogInformation(
            "PreConsume Message: {Message}, CorrelationId: {CorrelationId}, Namespace: {Namespace}",
            typeof(T).Name,
            context.CorrelationId,
            typeof(T).Namespace
        );
    }
}