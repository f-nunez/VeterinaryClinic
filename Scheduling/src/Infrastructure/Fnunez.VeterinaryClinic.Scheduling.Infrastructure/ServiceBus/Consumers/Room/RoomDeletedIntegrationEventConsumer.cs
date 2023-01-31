using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.ReceiveIntegrationEvents.RoomDeleted;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class RoomDeletedIntegrationEventConsumer
    : IConsumer<RoomDeletedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public RoomDeletedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Consume(
        ConsumeContext<RoomDeletedIntegrationEventContract> context)
    {
        var integrationEvent = new RoomDeletedReceiveIntegrationEvent(
            context.Message);

        return _mediator.Publish(integrationEvent);
    }
}