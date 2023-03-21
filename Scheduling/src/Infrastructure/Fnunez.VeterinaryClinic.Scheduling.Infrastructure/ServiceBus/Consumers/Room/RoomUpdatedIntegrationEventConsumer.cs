using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.ReceiveIntegrationEvents.RoomUpdated;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class RoomUpdatedIntegrationEventConsumer
    : IConsumer<RoomUpdatedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public RoomUpdatedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(
        ConsumeContext<RoomUpdatedIntegrationEventContract> context)
    {
        var integrationEvent = new RoomUpdatedReceiveIntegrationEvent(
            context.Message);

        await _mediator.Publish(integrationEvent, context.CancellationToken);
    }
}