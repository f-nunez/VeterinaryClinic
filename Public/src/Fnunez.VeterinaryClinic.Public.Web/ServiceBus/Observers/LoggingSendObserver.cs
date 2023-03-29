using MassTransit;

namespace Fnunez.VeterinaryClinic.Public.Web.ServiceBus.Observers;

public class LoggingSendObserver : ISendObserver
{
    private readonly ILogger<LoggingSendObserver> _logger;

    public LoggingSendObserver(ILogger<LoggingSendObserver> logger)
    {
        _logger = logger;
    }

    public async Task PostSend<T>(SendContext<T> context) where T : class
    {
        await Task.Yield();

        _logger.LogInformation(
            "PostSend Message: {Message}, CorrelationId: {CorrelationId}, Namespace: {Namespace}",
            typeof(T).Name,
            context.CorrelationId,
            typeof(T).Namespace
        );
    }

    public async Task PreSend<T>(SendContext<T> context) where T : class
    {
        await Task.Yield();

        _logger.LogInformation(
            "PreSend Message: {Message}, CorrelationId: {CorrelationId}, Namespace: {Namespace}",
            typeof(T).Name,
            context.CorrelationId,
            typeof(T).Namespace
        );
    }

    public async Task SendFault<T>(
        SendContext<T> context,
        Exception exception) where T : class
    {
        await Task.Yield();

        _logger.LogError(
            "SendFault Message: {Message}, CorrelationId: {CorrelationId}, Namespace: {Namespace}, Error: {Error}",
            typeof(T).Name,
            context.CorrelationId,
            typeof(T).Namespace,
            exception.Message
        );
    }
}