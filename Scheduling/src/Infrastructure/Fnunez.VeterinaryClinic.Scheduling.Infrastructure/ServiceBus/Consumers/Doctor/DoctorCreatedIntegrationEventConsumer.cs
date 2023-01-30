using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.ReceiveIntegrationEvents.DoctorCreated;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class DoctorCreatedIntegrationEventConsumer
    : IConsumer<DoctorCreatedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public DoctorCreatedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Consume(
        ConsumeContext<DoctorCreatedIntegrationEventContract> context)
    {
        var integrationEvent = new DoctorCreatedReceiveIntegrationEvent(
            context.Message);

        return _mediator.Publish(integrationEvent);
    }
}