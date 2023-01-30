using MassTransit;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.ServiceBus.Observers;

public class LoggingReceiveObserver : IReceiveObserver
{
    private static string ExchangeNameKey = "RabbitMQ-ExchangeName";
    private readonly ILogger<LoggingReceiveObserver> _logger;

    public LoggingReceiveObserver(ILogger<LoggingReceiveObserver> logger)
    {
        _logger = logger;
    }

    public async Task ConsumeFault<T>(
        ConsumeContext<T> context,
        TimeSpan duration,
        string consumerType,
        Exception exception) where T : class
    {
        await Task.Yield();

        _logger.LogError(
            "ConsumeFault Message: {Message}, CorrelationId: {CorrelationId}, Namespace: {Namespace}, Consumer: {Consumer}, Duration: {Duration} Error: {Error}",
            typeof(T).Name,
            context.CorrelationId,
            typeof(T).Namespace,
            consumerType,
            duration,
            exception.Message
        );
    }

    public async Task PostConsume<T>(
        ConsumeContext<T> context,
        TimeSpan duration,
        string consumerType) where T : class
    {
        await Task.Yield();

        _logger.LogInformation(
            "PostConsume Message: {Message}, CorrelationId: {CorrelationId}, Namespace: {Namespace}, Consumer: {Consumer}, Duration: {Duration}",
            typeof(T).Name,
            context.CorrelationId,
            typeof(T).Namespace,
            consumerType,
            duration
        );
    }

    public async Task PostReceive(ReceiveContext context)
    {
        await Task.Yield();

        _logger.LogInformation(
            "PostReceive Exchange: {Exchange}, CorrelationId: {CorrelationId}, Redelivered: {Redelivered}",
            context.TransportHeaders.Get<string>(ExchangeNameKey),
            context.GetCorrelationId(),
            context.Redelivered
        );
    }

    public async Task PreReceive(ReceiveContext context)
    {
        await Task.Yield();

        _logger.LogInformation(
            "PreReceive Exchange: {Exchange}, CorrelationId: {CorrelationId}, Redelivered: {Redelivered}",
            context.TransportHeaders.Get<string>(ExchangeNameKey),
            context.GetCorrelationId(),
            context.Redelivered
        );
    }

    public async Task ReceiveFault(ReceiveContext context, Exception exception)
    {
        await Task.Yield();

        _logger.LogError(
            "ReceiveFault Exchange: {Exchange}, CorrelationId: {CorrelationId}, Redelivered: {Redelivered}, Error: {Error}",
            context.TransportHeaders.Get<string>(ExchangeNameKey),
            context.GetCorrelationId(),
            context.Redelivered,
            exception.Message
        );
    }
}