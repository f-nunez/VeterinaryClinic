using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.CreateAppointment;

public record CreateAppointmentCommand(CreateAppointmentRequest CreateAppointmentRequest)
    : IRequest<CreateAppointmentResponse>;