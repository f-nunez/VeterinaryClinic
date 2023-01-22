using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterDuration;

public class GetAppointmentTypesFilterDurationQueryValidator
    : AbstractValidator<GetAppointmentTypesFilterDurationQuery>
{
    public GetAppointmentTypesFilterDurationQueryValidator()
    {
        RuleFor(v => v.GetAppointmentTypesFilterDurationRequest.DurationFilterValue)
            .NotNull().WithMessage("DurationFilterValue is required.")
            .NotEmpty().WithMessage("DurationFilterValue is required.");
    }
}