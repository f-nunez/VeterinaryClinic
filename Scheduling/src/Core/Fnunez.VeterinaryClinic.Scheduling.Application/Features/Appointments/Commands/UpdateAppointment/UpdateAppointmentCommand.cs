using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.UpdateAppointment;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.UpdateAppointment;

public record UpdateAppointmentCommand(UpdateAppointmentRequest UpdateAppointmentRequest)
    : IRequest<UpdateAppointmentResponse>;