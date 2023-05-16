using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypes;

public class GetAppointmentTypesQueryValidator
    : AbstractValidator<GetAppointmentTypesQuery>
{
    public GetAppointmentTypesQueryValidator()
    {
        RuleFor(v => v.GetAppointmentTypesRequest.CodeFilterValue)
            .MaximumLength(200).WithMessage("CodeFilterValue must not exceed 200 characters.");

        RuleFor(v => v.GetAppointmentTypesRequest.DataGridRequest.Search)
            .MaximumLength(200).WithMessage("Search must not exceed 200 characters.");

        RuleFor(v => v.GetAppointmentTypesRequest.DataGridRequest.Skip)
            .GreaterThanOrEqualTo(0).WithMessage("Skip must be greater than or equal to 0.");

        RuleFor(v => v.GetAppointmentTypesRequest.DataGridRequest.Take)
            .GreaterThan(0).WithMessage("Take must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Take must be less than or equal to 100.");

        RuleFor(v => v.GetAppointmentTypesRequest.DurationFilterValue)
            .MaximumLength(200).WithMessage("DurationFilterValue must not exceed 200 characters.");

        RuleFor(v => v.GetAppointmentTypesRequest.IdFilterValue)
            .MaximumLength(200).WithMessage("IdFilterValue must not exceed 200 characters.");

        RuleFor(v => v.GetAppointmentTypesRequest.NameFilterValue)
            .MaximumLength(200).WithMessage("NameFilterValue must not exceed 200 characters.");
    }
}