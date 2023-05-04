namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Common.Interfaces;

public interface IServiceBus
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default);
}