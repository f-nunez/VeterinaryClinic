using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.ReceiveIntegrationEvents.ClientUpdated;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class ClientUpdatedIntegrationEventConsumer
    : IConsumer<ClientUpdatedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public ClientUpdatedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(
        ConsumeContext<ClientUpdatedIntegrationEventContract> context)
    {
        var integrationEvent = new ClientUpdatedReceiveIntegrationEvent(
            context.Message);

        await _mediator.Publish(integrationEvent, context.CancellationToken);
    }
}