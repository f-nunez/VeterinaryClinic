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

    public async Task Handle(
        DoctorUpdatedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.DoctorUpdatedIntegrationEventContract;

        await _serviceBus.PublishAsync(message, cancellationToken);
    }
}