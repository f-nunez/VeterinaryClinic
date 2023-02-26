namespace Fnunez.VeterinaryClinic.Scheduling.Application.Common.Interfaces;

public interface IServiceBus
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken);
}