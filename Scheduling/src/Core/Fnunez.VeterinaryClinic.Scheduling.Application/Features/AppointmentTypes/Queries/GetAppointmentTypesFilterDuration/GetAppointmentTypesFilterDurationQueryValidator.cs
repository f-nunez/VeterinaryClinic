using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterDuration;

public class GetAppointmentTypesFilterDurationQueryValidator
    : AbstractValidator<GetAppointmentTypesFilterDurationQuery>
{
    public GetAppointmentTypesFilterDurationQueryValidator()
    {
        RuleFor(v => v.GetAppointmentTypesFilterDurationRequest.DurationFilterValue)
            .NotNull().WithMessage("DurationFilterValue is required.")
            .NotEmpty().WithMessage("DurationFilterValue is required.")
            .MaximumLength(200).WithMessage("DurationFilterValue must not exceed 200 characters.");
    }
}