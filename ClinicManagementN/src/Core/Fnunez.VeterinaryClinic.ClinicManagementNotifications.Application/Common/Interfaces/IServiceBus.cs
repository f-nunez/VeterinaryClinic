namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Interfaces;

public interface IServiceBus
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default);
}