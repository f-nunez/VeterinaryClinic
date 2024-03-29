using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentById;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentById;

public record GetAppointmentByIdQuery(GetAppointmentByIdRequest GetAppointmentByIdRequest)
    : IRequest<GetAppointmentByIdResponse>;