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

    public Task Consume(
        ConsumeContext<RoomUpdatedIntegrationEventContract> context)
    {
        var integrationEvent = new RoomUpdatedReceiveIntegrationEvent(
            context.Message);

        return _mediator.Publish(integrationEvent);
    }
}