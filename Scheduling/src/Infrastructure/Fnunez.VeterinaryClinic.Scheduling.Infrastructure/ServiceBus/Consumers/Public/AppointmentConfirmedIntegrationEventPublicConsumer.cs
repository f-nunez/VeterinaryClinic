using Contracts.Public;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.ReceiveIntegrationEvents.AppointmentConfirmed;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class AppointmentConfirmedIntegrationEventPublicConsumer
    : IConsumer<AppointmentConfirmedIntegrationEventPublicContract>
{
    private readonly IMediator _mediator;

    public AppointmentConfirmedIntegrationEventPublicConsumer(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(
        ConsumeContext<AppointmentConfirmedIntegrationEventPublicContract> context)
    {
        var integrationEvent = new AppointmentConfirmedReceiveIntegrationEvent(
            context.Message);

        await _mediator.Publish(integrationEvent, context.CancellationToken);
    }
}