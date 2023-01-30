using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.ReceiveIntegrationEvents.DoctorDeleted;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class DoctorDeletedIntegrationEventConsumer
    : IConsumer<DoctorDeletedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public DoctorDeletedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Consume(
        ConsumeContext<DoctorDeletedIntegrationEventContract> context)
    {
        var integrationEvent = new DoctorDeletedReceiveIntegrationEvent(
            context.Message);

        return _mediator.Publish(integrationEvent);
    }
}