using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctorsFilterFullName;

public class GetDoctorsFilterFullNameQueryValidator
    : AbstractValidator<GetDoctorsFilterFullNameQuery>
{
    public GetDoctorsFilterFullNameQueryValidator()
    {
        RuleFor(v => v.GetDoctorsFilterFullNameRequest.FullNameFilterValue)
            .NotNull().WithMessage("FullNameFilterValue is required.")
            .NotEmpty().WithMessage("FullNameFilterValue is required.")
            .MaximumLength(200).WithMessage("FullNameFilterValue must not exceed 200 characters.");
    }
}