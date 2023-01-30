using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.ReceiveIntegrationEvents.DoctorUpdated;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class DoctorUpdatedIntegrationEventConsumer
    : IConsumer<DoctorUpdatedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public DoctorUpdatedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Consume(
        ConsumeContext<DoctorUpdatedIntegrationEventContract> context)
    {
        var integrationEvent = new DoctorUpdatedReceiveIntegrationEvent(
            context.Message);

        return _mediator.Publish(integrationEvent);
    }
}