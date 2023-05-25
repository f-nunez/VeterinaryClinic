using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.ReceiveIntegrationEvents.ClientDeleted;

public record ClientDeletedReceiveIntegrationEvent(ClientDeletedIntegrationEvent ClientDeletedIntegrationEvent)
    : INotification;