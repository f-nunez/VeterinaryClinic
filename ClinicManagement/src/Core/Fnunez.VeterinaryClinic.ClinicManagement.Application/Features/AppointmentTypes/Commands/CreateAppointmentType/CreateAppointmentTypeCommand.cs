using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.CreateAppointmentType;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.CreateAppointmentType;

public record CreateAppointmentTypeCommand(CreateAppointmentTypeRequest CreateAppointmentTypeRequest)
    : IRequest<CreateAppointmentTypeResponse>;
