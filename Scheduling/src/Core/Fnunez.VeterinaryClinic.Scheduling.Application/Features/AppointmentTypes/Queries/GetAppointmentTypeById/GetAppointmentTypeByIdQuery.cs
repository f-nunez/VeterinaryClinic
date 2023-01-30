using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypeById;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypeById;

public record GetAppointmentTypeByIdQuery(GetAppointmentTypeByIdRequest GetAppointmentTypeByIdRequest)
    : IRequest<GetAppointmentTypeByIdResponse>;