using MassTransit;

namespace Fnunez.VeterinaryClinic.EmailService.Api.ServiceBus;

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