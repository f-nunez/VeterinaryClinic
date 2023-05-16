using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctors;

public class GetDoctorsQueryValidator : AbstractValidator<GetDoctorsQuery>
{
    public GetDoctorsQueryValidator()
    {
        RuleFor(v => v.GetDoctorsRequest.DataGridRequest.Search)
            .MaximumLength(200).WithMessage("Search must not exceed 200 characters.");

        RuleFor(v => v.GetDoctorsRequest.DataGridRequest.Skip)
            .GreaterThanOrEqualTo(0).WithMessage("Skip must be greater than or equal to 0.");

        RuleFor(v => v.GetDoctorsRequest.DataGridRequest.Take)
            .GreaterThan(0).WithMessage("Take must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Take must be less than or equal to 100.");

        RuleFor(v => v.GetDoctorsRequest.FullNameFilterValue)
            .MaximumLength(200).WithMessage("FullNameFilterValue must not exceed 200 characters.");

        RuleFor(v => v.GetDoctorsRequest.IdFilterValue)
            .MaximumLength(200).WithMessage("IdFilterValue must not exceed 200 characters.");
    }
}