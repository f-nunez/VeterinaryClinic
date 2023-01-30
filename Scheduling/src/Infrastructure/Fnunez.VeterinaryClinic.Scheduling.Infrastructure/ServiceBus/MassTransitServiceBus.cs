using Fnunez.VeterinaryClinic.Scheduling.Application.Interfaces.ServiceBus;
using MassTransit;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus;

public class MassTransitServiceBus : IServiceBus
{
    public IPublishEndpoint _publishEndpoint { get; set; }

    public MassTransitServiceBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task PublishAsync<TMessage>(
        TMessage message,
        CancellationToken cancellationToken)
    {
        if (message is null)
            throw new ArgumentNullException(nameof(message));

        return Task.WhenAll(
            _publishEndpoint.Publish(message, cancellationToken)
        );
    }
}