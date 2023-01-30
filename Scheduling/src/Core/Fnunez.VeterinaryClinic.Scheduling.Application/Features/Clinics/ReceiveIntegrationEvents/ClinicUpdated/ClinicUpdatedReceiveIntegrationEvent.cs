using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.ReceiveIntegrationEvents.ClinicUpdated;

public record ClinicUpdatedReceiveIntegrationEvent(ClinicUpdatedIntegrationEventContract ClinicUpdatedIntegrationEventContract)
    : INotification;