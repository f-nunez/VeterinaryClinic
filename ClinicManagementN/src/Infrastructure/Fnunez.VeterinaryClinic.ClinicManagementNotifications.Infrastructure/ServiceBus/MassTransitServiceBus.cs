using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Interfaces;
using MassTransit;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Infrastructure.ServiceBus;

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