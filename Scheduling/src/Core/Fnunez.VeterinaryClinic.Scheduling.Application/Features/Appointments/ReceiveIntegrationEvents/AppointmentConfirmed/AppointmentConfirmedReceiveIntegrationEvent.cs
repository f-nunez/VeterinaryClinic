using MediatR;
using Contracts.Public;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.ReceiveIntegrationEvents.AppointmentConfirmed;

public record AppointmentConfirmedReceiveIntegrationEvent(AppointmentConfirmedIntegrationEventPublicContract AppointmentConfirmedIntegrationEventPublicContract)
    : INotification;