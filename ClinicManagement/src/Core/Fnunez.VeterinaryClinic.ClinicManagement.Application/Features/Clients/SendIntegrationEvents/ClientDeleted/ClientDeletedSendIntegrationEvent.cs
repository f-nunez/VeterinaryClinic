using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.SendIntegrationEvents.ClientDeleted;

public record ClientDeletedSendIntegrationEvent(ClientDeletedIntegrationEventContract ClientDeletedIntegrationEventContract)
    : INotification;