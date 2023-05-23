using Contracts.ClinicManagement;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver;
using MassTransit;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class IntegrationEventClinicManagementConsumer
    : IConsumer<IntegrationEventClinicManagementContract>
{
    private readonly IIntegrationEventReceiverService _integrationEventReceiverService;

    public IntegrationEventClinicManagementConsumer(
        IIntegrationEventReceiverService integrationEventReceiverService)
    {
        _integrationEventReceiverService = integrationEventReceiverService;
    }

    public async Task Consume(
        ConsumeContext<IntegrationEventClinicManagementContract> context)
    {
        await _integrationEventReceiverService.ReceiveAsync(
            context.Message.IntegrationEvent,
            context.Message.SerializedIntegrationEvent,
            context.CancellationToken
        );
    }
}