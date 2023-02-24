using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.SendIntegrationEvents.DoctorDeleted;

public class DoctorDeletedSendIntegrationEventHandler
    : INotificationHandler<DoctorDeletedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public DoctorDeletedSendIntegrationEventHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public Task Handle(
        DoctorDeletedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.DoctorDeletedIntegrationEventContract;

        return _serviceBus.PublishAsync(message, cancellationToken);
    }
}