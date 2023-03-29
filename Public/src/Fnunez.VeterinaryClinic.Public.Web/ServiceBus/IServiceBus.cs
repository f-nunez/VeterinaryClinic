namespace Fnunez.VeterinaryClinic.Public.Web.ServiceBus;

public interface IServiceBus
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default);
}