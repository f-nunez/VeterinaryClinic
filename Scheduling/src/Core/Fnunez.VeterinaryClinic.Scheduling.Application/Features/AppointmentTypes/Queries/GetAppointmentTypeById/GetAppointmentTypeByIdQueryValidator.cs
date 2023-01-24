using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypeById;

public class GetAppointmentTypeByIdQueryValidator
    : AbstractValidator<GetAppointmentTypeByIdQuery>
{
    public GetAppointmentTypeByIdQueryValidator()
    {
        RuleFor(v => v.GetAppointmentTypeByIdRequest.Id)
            .GreaterThan(0).WithMessage("Id is required.");
    }
}