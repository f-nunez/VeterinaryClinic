using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.SendIntegrationEvents.DoctorCreated;

public class DoctorCreatedSendIntegrationEventHandler
    : INotificationHandler<DoctorCreatedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public DoctorCreatedSendIntegrationEventHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public Task Handle(
        DoctorCreatedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.DoctorCreatedIntegrationEventContract;

        return _serviceBus.PublishAsync(message, cancellationToken);
    }
}