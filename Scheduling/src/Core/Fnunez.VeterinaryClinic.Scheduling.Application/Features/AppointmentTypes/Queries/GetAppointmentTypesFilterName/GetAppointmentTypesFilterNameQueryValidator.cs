using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterName;

public class GetAppointmentTypesFilterNameQueryValidator
    : AbstractValidator<GetAppointmentTypesFilterNameQuery>
{
    public GetAppointmentTypesFilterNameQueryValidator()
    {
        RuleFor(v => v.GetAppointmentTypesFilterNameRequest.NameFilterValue)
            .NotNull().WithMessage("NameFilterValue is required.")
            .NotEmpty().WithMessage("NameFilterValue is required.")
            .MaximumLength(200).WithMessage("NameFilterValue must not exceed 200 characters.");
    }
}