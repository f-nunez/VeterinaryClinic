using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.ReceiveIntegrationEvents.DoctorUpdated;

public record DoctorUpdatedReceiveIntegrationEvent(DoctorUpdatedIntegrationEventContract DoctorUpdatedIntegrationEventContract)
    : INotification;