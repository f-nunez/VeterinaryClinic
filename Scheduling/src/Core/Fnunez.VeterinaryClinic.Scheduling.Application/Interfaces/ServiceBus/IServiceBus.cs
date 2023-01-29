namespace Fnunez.VeterinaryClinic.Scheduling.Application.Interfaces.ServiceBus;

public interface IServiceBus
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken);
}