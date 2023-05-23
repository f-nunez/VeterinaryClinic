using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.ReceiveIntegrationEvents.ClientUpdated;

public record ClientUpdatedReceiveIntegrationEvent(ClientUpdatedIntegrationEvent ClientUpdatedIntegrationEvent)
    : INotification;