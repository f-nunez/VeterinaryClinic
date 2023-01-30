using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.ReceiveIntegrationEvents.RoomCreated;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class RoomCreatedIntegrationEventConsumer
    : IConsumer<RoomCreatedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public RoomCreatedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Consume(
        ConsumeContext<RoomCreatedIntegrationEventContract> context)
    {
        var integrationEvent = new RoomCreatedReceiveIntegrationEvent(
            context.Message);

        return _mediator.Publish(integrationEvent);
    }
}