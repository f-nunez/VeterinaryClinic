using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentDetail;

public record GetAppointmentDetailQuery(GetAppointmentDetailRequest GetAppointmentDetailRequest)
    : IRequest<GetAppointmentDetailResponse>;