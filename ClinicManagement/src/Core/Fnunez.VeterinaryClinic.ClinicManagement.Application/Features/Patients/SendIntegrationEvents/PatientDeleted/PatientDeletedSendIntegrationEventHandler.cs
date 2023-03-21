using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.SendIntegrationEvents.PatientDeleted;

public class PatientDeletedSendIntegrationEventHandler
    : INotificationHandler<PatientDeletedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public PatientDeletedSendIntegrationEventHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public async Task Handle(
        PatientDeletedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.PatientDeletedIntegrationEventContract;

        await _serviceBus.PublishAsync(message, cancellationToken);
    }
}