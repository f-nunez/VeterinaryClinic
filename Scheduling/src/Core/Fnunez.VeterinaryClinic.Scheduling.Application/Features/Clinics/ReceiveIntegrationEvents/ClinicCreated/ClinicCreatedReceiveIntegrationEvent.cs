using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.ReceiveIntegrationEvents.ClinicCreated;

public record ClinicCreatedReceiveIntegrationEvent(ClinicCreatedIntegrationEventContract ClinicCreatedIntegrationEventContract)
    : INotification;