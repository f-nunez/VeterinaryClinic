using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypes;

public record GetAppointmentTypesQuery(GetAppointmentTypesRequest GetAppointmentTypesRequest)
    : IRequest<GetAppointmentTypesResponse>;