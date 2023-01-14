using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypeById;

public class GetAppointmentTypeByIdQueryValidator
    : AbstractValidator<GetAppointmentTypeByIdQuery>
{
    public GetAppointmentTypeByIdQueryValidator()
    {
        RuleFor(v => v.GetAppointmentTypeByIdRequest.Id)
            .GreaterThan(0).WithMessage("Id is required.");
    }
}