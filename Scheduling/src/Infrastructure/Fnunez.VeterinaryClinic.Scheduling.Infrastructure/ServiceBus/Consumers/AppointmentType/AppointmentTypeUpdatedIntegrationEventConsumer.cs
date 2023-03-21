using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.ReceiveIntegrationEvents.AppointmentTypeUpdated;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class AppointmentTypeUpdatedIntegrationEventConsumer
    : IConsumer<AppointmentTypeUpdatedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public AppointmentTypeUpdatedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(
        ConsumeContext<AppointmentTypeUpdatedIntegrationEventContract> context)
    {
        var integrationEvent = new AppointmentTypeUpdatedReceiveIntegrationEvent(
            context.Message);

        await _mediator.Publish(integrationEvent, context.CancellationToken);
    }
}