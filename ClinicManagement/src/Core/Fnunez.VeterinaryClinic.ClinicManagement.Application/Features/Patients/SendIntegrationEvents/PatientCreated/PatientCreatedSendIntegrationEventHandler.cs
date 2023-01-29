using Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.ServiceBus;
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

    public Task Handle(
        PatientCreatedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.PatientCreatedIntegrationEventContract;

        return _serviceBus.PublishAsync(message, cancellationToken);
    }
}