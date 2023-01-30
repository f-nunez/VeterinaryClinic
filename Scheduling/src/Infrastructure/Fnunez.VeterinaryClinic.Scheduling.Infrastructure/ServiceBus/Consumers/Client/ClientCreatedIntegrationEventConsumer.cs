using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.ReceiveIntegrationEvents.ClientCreated;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class ClientCreatedIntegrationEventConsumer
    : IConsumer<ClientCreatedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public ClientCreatedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Consume(
        ConsumeContext<ClientCreatedIntegrationEventContract> context)
    {
        var integrationEvent = new ClientCreatedReceiveIntegrationEvent(
            context.Message);

        return _mediator.Publish(integrationEvent);
    }
}