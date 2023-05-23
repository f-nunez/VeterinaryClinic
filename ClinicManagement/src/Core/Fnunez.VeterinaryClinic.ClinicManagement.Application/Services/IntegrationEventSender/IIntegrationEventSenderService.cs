using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;

public interface IIntegrationEventSenderService
{
    Task SendAsync(IIntegrationEventFactory factory, Guid correlationId, CancellationToken cancellationToken);
}