using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterName;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterName;

public record GetAppointmentTypesFilterNameQuery(GetAppointmentTypesFilterNameRequest GetAppointmentTypesFilterNameRequest)
    : IRequest<GetAppointmentTypesFilterNameResponse>;