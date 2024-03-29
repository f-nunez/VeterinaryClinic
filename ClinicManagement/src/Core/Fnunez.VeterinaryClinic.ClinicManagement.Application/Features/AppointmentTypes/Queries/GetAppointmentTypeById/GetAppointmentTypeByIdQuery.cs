using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypeById;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypeById;

public record GetAppointmentTypeByIdQuery(GetAppointmentTypeByIdRequest GetAppointmentTypeByIdRequest)
    : IRequest<GetAppointmentTypeByIdResponse>;