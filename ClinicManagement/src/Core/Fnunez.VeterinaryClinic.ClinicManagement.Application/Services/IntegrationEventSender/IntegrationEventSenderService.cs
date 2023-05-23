using System.Text.Json;
using Contracts.ClinicManagement;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;

public class IntegrationEventSenderService : IIntegrationEventSenderService
{
    private readonly IServiceBus _serviceBus;

    public IntegrationEventSenderService(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public async Task SendAsync(
        IIntegrationEventFactory factory,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var integrationEvent = factory.CreateIntegrationEvent();

        var message = new IntegrationEventClinicManagementContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = correlationId,
            IntegrationEvent = factory.GetIntegrationEvent(),
            OccurredOn = DateTimeOffset.UtcNow,
            SerializedIntegrationEvent = JsonSerializer.Serialize((object)integrationEvent)
        };

        await _serviceBus.PublishAsync(message, cancellationToken);
    }
}