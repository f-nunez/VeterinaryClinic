namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver;

public interface IIntegrationEventReceiverService
{
    public Task ReceiveAsync(string integrationEventString, string serializedIntegrationEvent, CancellationToken cancellationToken);
}