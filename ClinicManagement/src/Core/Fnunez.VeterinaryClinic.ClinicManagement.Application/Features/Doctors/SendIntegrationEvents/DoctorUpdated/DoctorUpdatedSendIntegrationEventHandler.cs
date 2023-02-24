using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.SendIntegrationEvents.DoctorUpdated;

public class DoctorUpdatedSendIntegrationEventHandler
    : INotificationHandler<DoctorUpdatedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public DoctorUpdatedSendIntegrationEventHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public Task Handle(
        DoctorUpdatedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.DoctorUpdatedIntegrationEventContract;

        return _serviceBus.PublishAsync(message, cancellationToken);
    }
}