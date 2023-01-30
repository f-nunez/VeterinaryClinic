namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.ServiceBus;

public interface IServiceBus
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default);
}