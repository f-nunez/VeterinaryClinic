using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.ReceiveIntegrationEvents.ClientDeleted;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class ClientDeletedIntegrationEventConsumer
    : IConsumer<ClientDeletedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public ClientDeletedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Consume(
        ConsumeContext<ClientDeletedIntegrationEventContract> context)
    {
        var integrationEvent = new ClientDeletedReceiveIntegrationEvent(
            context.Message);

        return _mediator.Publish(integrationEvent);
    }
}