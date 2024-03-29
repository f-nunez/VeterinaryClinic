using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.ReceiveIntegrationEvents.ClientCreated;

public record ClientCreatedReceiveIntegrationEvent(ClientCreatedIntegrationEvent ClientCreatedIntegrationEvent)
    : INotification;