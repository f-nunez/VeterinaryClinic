using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Interfaces;
using MassTransit;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus;

public class MassTransitServiceBus : IServiceBus
{
    public IPublishEndpoint _publishEndpoint { get; set; }

    public MassTransitServiceBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishAsync<TMessage>(
        TMessage message,
        CancellationToken cancellationToken)
    {
        if (message is null)
            throw new ArgumentNullException(nameof(message));

        await _publishEndpoint.Publish(message, cancellationToken);
    }
}