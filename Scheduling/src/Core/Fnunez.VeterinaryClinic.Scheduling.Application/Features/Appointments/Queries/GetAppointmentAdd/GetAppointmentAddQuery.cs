using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentAdd;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentAdd;

public record GetAppointmentAddQuery(GetAppointmentAddRequest GetAppointmentAddRequest)
    : IRequest<GetAppointmentAddResponse>;