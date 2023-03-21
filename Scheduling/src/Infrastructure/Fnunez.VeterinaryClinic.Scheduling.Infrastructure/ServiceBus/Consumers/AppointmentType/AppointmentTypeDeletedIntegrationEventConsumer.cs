using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.ReceiveIntegrationEvents.AppointmentTypeDeleted;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class AppointmentTypeDeletedIntegrationEventConsumer
    : IConsumer<AppointmentTypeDeletedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public AppointmentTypeDeletedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(
        ConsumeContext<AppointmentTypeDeletedIntegrationEventContract> context)
    {
        var integrationEvent = new AppointmentTypeDeletedReceiveIntegrationEvent(
            context.Message);

        await _mediator.Publish(integrationEvent, context.CancellationToken);
    }
}