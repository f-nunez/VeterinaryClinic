using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypes;

public record GetAppointmentTypesQuery(GetAppointmentTypesRequest GetAppointmentTypesRequest)
    : IRequest<GetAppointmentTypesResponse>;