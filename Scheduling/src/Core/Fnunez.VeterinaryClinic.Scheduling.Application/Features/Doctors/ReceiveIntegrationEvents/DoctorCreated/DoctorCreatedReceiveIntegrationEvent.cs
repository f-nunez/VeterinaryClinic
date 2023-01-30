using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.ReceiveIntegrationEvents.DoctorCreated;

public record DoctorCreatedReceiveIntegrationEvent(DoctorCreatedIntegrationEventContract DoctorCreatedIntegrationEventContract)
    : INotification;