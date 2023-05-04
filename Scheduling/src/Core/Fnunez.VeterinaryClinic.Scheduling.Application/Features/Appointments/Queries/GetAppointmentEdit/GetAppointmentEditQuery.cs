using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentEdit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentEdit;

public record GetAppointmentEditQuery(GetAppointmentEditRequest GetAppointmentEditRequest)
    : IRequest<GetAppointmentEditResponse>;