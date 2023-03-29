namespace Fnunez.VeterinaryClinic.EmailService.Api.ServiceBus;

public interface IServiceBus
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default);
}