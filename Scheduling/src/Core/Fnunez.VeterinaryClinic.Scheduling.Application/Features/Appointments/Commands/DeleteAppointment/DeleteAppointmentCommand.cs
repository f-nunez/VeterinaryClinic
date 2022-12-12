using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.DeleteAppointment;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.DeleteAppointment;

public record DeleteAppointmentCommand(DeleteAppointmentRequest DeleteAppointmentRequest) : IRequest<DeleteAppointmentResponse>;