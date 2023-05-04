using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointments;

public record GetAppointmentsQuery(GetAppointmentsRequest GetAppointmentsRequest)
    : IRequest<GetAppointmentsResponse>;