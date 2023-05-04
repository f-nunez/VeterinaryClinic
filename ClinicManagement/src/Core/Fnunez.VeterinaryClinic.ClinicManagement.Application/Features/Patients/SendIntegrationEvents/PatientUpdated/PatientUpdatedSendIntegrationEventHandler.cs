using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.SendIntegrationEvents.PatientUpdated;

public class PatientUpdatedSendIntegrationEventHandler
    : INotificationHandler<PatientUpdatedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public PatientUpdatedSendIntegrationEventHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public async Task Handle(
        PatientUpdatedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.PatientUpdatedIntegrationEventContract;

        await _serviceBus.PublishAsync(message, cancellationToken);
    }
}