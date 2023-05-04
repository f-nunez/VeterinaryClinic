using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.ReceiveIntegrationEvents.AppointmentTypeCreated;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class AppointmentTypeCreatedIntegrationEventConsumer
    : IConsumer<AppointmentTypeCreatedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public AppointmentTypeCreatedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(
        ConsumeContext<AppointmentTypeCreatedIntegrationEventContract> context)
    {
        var integrationEvent = new AppointmentTypeCreatedReceiveIntegrationEvent(
            context.Message);

        await _mediator.Publish(integrationEvent, context.CancellationToken);
    }
}