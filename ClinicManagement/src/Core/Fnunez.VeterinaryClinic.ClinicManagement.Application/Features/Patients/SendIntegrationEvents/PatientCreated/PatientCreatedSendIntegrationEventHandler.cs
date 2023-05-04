using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.SendIntegrationEvents.PatientCreated;

public class PatientCreatedSendIntegrationEventHandler
    : INotificationHandler<PatientCreatedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public PatientCreatedSendIntegrationEventHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public async Task Handle(
        PatientCreatedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.PatientCreatedIntegrationEventContract;

        await _serviceBus.PublishAsync(message, cancellationToken);
    }
}