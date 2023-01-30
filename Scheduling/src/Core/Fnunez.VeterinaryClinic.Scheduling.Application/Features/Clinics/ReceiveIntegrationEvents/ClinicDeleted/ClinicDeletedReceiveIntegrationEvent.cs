using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.ReceiveIntegrationEvents.ClinicDeleted;

public record ClinicDeletedReceiveIntegrationEvent(ClinicDeletedIntegrationEventContract ClinicDeletedIntegrationEventContract)
    : INotification;